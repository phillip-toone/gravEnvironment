Public Class frmMain

   Public Logger As New HumidityMeter()
   Public TestMode As Boolean
    Dim FileName As String 'The path to the file recording historic data
    Dim RobotDataFileName As String  'The file where the robot data is saved to
   Dim Interval_min As Int32
   Dim WithEvents RunTimer As New Windows.Forms.Timer
   Dim LogTime As DateTime
   Dim ChartLength As Int32   'Measured in number of weeks


   Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
      TestMode = False

      If TestMode = True Then
         Logger.TestMode = True
      End If
      RunTimer.Interval = 500
      AddHandler RunTimer.Tick, AddressOf StartUpTimer_Tick
      RunTimer.Start()

   End Sub

   Private Sub StartUpTimer_Tick(sender As Object, e As EventArgs)
      RunTimer.Stop()

      If Logger.Initialize() = False Then
         MessageBox.Show("The comm port for the humidity meter did not open.", "Port Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
         Me.Close()
      End If
      Interval_min = My.Settings.LogInterval
      txbInterval.Text = Interval_min.ToString
      LogTime = Now.AddMinutes(Interval_min)

      FileName = My.Settings.ProgramDataPath
      txbDirectory.Text = FileName
      RobotDataFileName = My.Settings.RobotDataPath
      Logger.GetReadings()
      lblTemperature.Text = Format(Logger.Temperature, "0.0") & " C"
      lblHumidity.Text = Format(Logger.Humidity, "0.0") & " %"
      cbxChartLength.SelectedIndex = 4  'three months
      ChartLength = 13  'Defautl on start up is 3 months
      If My.Computer.FileSystem.FileExists(FileName) Then
         RecallData()
      Else
         ChartDataPoint(Now, Logger.Temperature, Logger.Humidity)
      End If
      Application.DoEvents()

      AddHandler txbInterval.TextChanged, AddressOf txbInterval_TextChanged
      RemoveHandler RunTimer.Tick, AddressOf StartUpTimer_Tick
      AddHandler RunTimer.Tick, AddressOf RunTimer_Tick
      AddHandler cbxChartLength.SelectedIndexChanged, AddressOf cbxChartLength_SelectedIndexChanged
      RunTimer.Interval = 60000
      RunTimer.Start()
   End Sub

   Private Sub RunTimer_Tick(sender As Object, e As EventArgs)
      'Update the screen every minute
      Logger.GetReadings()
      lblTemperature.Text = Format(Logger.Temperature, "0.0") & " C"
      lblHumidity.Text = Format(Logger.Humidity, "0.0") & " %"
      If DateTime.Compare(Now, LogTime) >= 0 Then    'Log data at every logtime
         LogTime = Now.AddMinutes(Interval_min - 0.01)
         LogDataPoint(Logger.Temperature, Logger.Humidity, Logger.BarometricPressure)
         ChartDataPoint(Now, Logger.Temperature, Logger.Humidity)
         Application.DoEvents()
      End If
   End Sub


   Sub RecallData(ByVal Optional updateChart As Boolean = True)
      'If they exit and then restart the program, this reloads the last 3 months of data.
      Dim FileData() As String
      Dim Linedata() As String
      Dim Pass As Int32
      Dim ThisDate As DateTime
      Dim StepSize As Int32
      Dim StartIndex As Int32

      Try
         Chart1.Series("Temperature").Points.Clear()
         Chart1.Series("Humidity").Points.Clear()
         FileData = FileIO.ReadTextFile(FileName).Split(Chr(10))  'Split on LF
         'Find the first data point newer than the chart time lenght
         For Pass = 1 To FileData.Length - 2
            Linedata = FileData(Pass).Split(Chr(44))
            ThisDate = DateTime.FromOADate(CDbl(Linedata(4)))
            If ThisDate > Now.AddDays(-ChartLength * 7) Then
               StartIndex = Pass
               Exit For   'This is the first index with the age specifed
            End If
         Next

         StepSize = ((FileData.Length - StartIndex) \ 10000) + 1    'Dont plot more than 10000 points
         Try
            For Pass = StartIndex To FileData.Length - 2 Step StepSize
               'Looks like this:   8/24/2021 at 5:24:23 PM,21.2,37.1,648.3,12345.67891234,CrLf   Where the last long digit is the AODate time
               Linedata = FileData(Pass).Split(Chr(44))
               ThisDate = DateTime.FromOADate(CDbl(Linedata(4)))
               Chart1.Series("Temperature").Points.AddXY(ThisDate, CDbl(Linedata(1)))
               Chart1.Series("Humidity").Points.AddXY(ThisDate, CDbl(Linedata(2)))
            Next
         Catch ex As Exception
         End Try

         If updateChart = True Then
            ChartDataPoint(Now, Logger.Temperature, Logger.Humidity)
         End If

      Catch ex As Exception
      End Try
   End Sub


   Sub ChartDataPoint(ByVal Time As DateTime, ByVal temp As Double, ByVal humidity As Double)
      Dim ChartStart As Double
      Dim ChartStop As Double

      Chart1.Series("Temperature").Points.AddXY(Time, temp)
      Chart1.Series("Humidity").Points.AddXY(Time, humidity)

      ChartStart = Chart1.Series("Temperature").Points(0).XValue
      ChartStop = Chart1.Series("Temperature").Points(Chart1.Series("Temperature").Points.Count - 1).XValue
      Chart1.ChartAreas("ChartArea1").AxisX.MajorTickMark.Interval = (ChartStop - ChartStart) / 7.0
      Chart1.ChartAreas("ChartArea1").AxisX.MinorTickMark.Interval = Chart1.ChartAreas("ChartArea1").AxisX.MajorTickMark.Interval / 10.0
      Chart1.ChartAreas("ChartArea1").AxisX.MajorTickMark.Enabled = True
      Chart1.ChartAreas("ChartArea1").AxisX.MinorTickMark.Enabled = True
      Chart1.ChartAreas("ChartArea1").AxisX.MinorGrid.LineColor = Color.PaleTurquoise

      'The X axis is storred in AOtime.
      If Chart1.Series("Temperature").Points(0).XValue < Time.ToOADate - (ChartLength * 7) Then   'Chart lenght is in units of weeks.  The integer portion of time.tooadate is days
         Chart1.Series("Temperature").Points.RemoveAt(0)
         Chart1.Series("Humidity").Points.RemoveAt(0)
      End If
      If Chart1.Series("Temperature").Points.Count > 10000 Then   'displaying 10,000 points is the limit that I allow
         RecallData(False)   'This will clear the chart series and reload the displayed data with fewer data points
      End If

      ChartStart = Chart1.Series("Temperature").Points(0).XValue
      ChartStop = Chart1.Series("Temperature").Points(Chart1.Series("Temperature").Points.Count - 1).XValue
      Chart1.ChartAreas("ChartArea1").AxisX.Minimum = ChartStart
      Chart1.ChartAreas("ChartArea1").AxisX.Maximum = ChartStop + (ChartStop - ChartStart) / 100.0   'Make the lenght of the axis 1% longer than the displayed range


      Chart1.Invalidate()
   End Sub

   Sub LogDataPoint(ByVal temp As Double, ByVal humidity As Double, ByVal pressure As Double)
      Dim Data As String

      Data = Now.ToShortDateString & " at " & Now.ToLongTimeString & "," & Format(temp, "0.0") & "," & Format(humidity, "0.0") & "," & Format(pressure, "0.0") & "," & Now.ToOADate.ToString & "," & vbCrLf
      FileIO.WriteTextFile(FileName, Data, False, True)
      Try
         'Write the data points to a file that the robot program will read to get the humidity and temperature data.
         FileIO.WriteTextFile(RobotDataFileName, Format(temp, "0.0") & "," & Format(humidity, "0.0") & "," & vbCrLf, True, False)
      Catch ex As Exception
      End Try
   End Sub


   Private Sub btnLocate_Click(sender As Object, e As EventArgs) Handles btnLocate.Click
      Dim OFD As New FolderBrowserDialog

      OFD.Description = "Data File Path"
      OFD.ShowDialog()
      If OFD.SelectedPath <> "" Then
         If gbxInfo.Text.Contains("Program") Then
            'This is the directory where log data for this application is saved.
            FileName = OFD.SelectedPath & "\HumidityData.csv"
            If My.Computer.FileSystem.FileExists(FileName) = True Then
               Exit Sub   'The file already exists
            Else
               FileIO.WriteTextFile(FileName, "Time,Temperature (C),Relitive Humidity (%),Barometric Pressure (mmHg)" & vbCrLf, False, False)
               txbDirectory.Text = FileName
               My.Settings.ProgramDataPath = FileName
               My.Settings.Save()
            End If
         Else
            'This program saves the last temperatuer and humidity reading to a file in the Data folder associated with the robot.   The user must locate this folder
            txbDirectory.Text = OFD.SelectedPath & "\Humidity.txt"
            My.Settings.RobotDataPath = txbDirectory.Text
            FileName = txbDirectory.Text
            My.Settings.Save()
         End If
         MessageBox.Show("To save data to this new file location you must exit and then restart the software.", "New File Location", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
      End If
      gbxInfo.Visible = False
   End Sub

   Private Sub mnuDataPath_Click(sender As Object, e As EventArgs) Handles mnuDataPath.Click
      gbxInfo.Size = New Size(559, 148)
      gbxInfo.Location = New Point(233, 105)
      lblMessage.Text = "Identify the location where Time, Temperature, And Humidity data" & vbCrLf & "should be saved to."
      gbxInfo.Text = "Program Data File"
      gbxInfo.Visible = True
   End Sub

   Private Sub mnuRobotDataPath_Click(sender As Object, e As EventArgs) Handles mnuRobotDataPath.Click
      If MessageBox.Show("NO!! Don't change this.   This data  path is required for the robot to read temperature and humidity data.   If you do, I'll tell Scott on you." & vbCrLf & "Do you want to change this data path?", "Don't Do It!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
         If MessageBox.Show("Are you really sure?", "Really?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
            gbxInfo.Size = New Size(559, 148)
            gbxInfo.Location = New Point(233, 105)
            lblMessage.Text = "Identify the location where Time, Temperature, And Humidity data" & vbCrLf & "should be saved to."
            lblMessage.Text = "This program requires that the robots data folder be identified." & vbCrLf & "Locate the ROBOT's data folder now."
            gbxInfo.Text = "Robot Data File"
            gbxInfo.Visible = True
         End If
      End If
   End Sub

   Private Sub mnuEnable_Click(sender As Object, e As EventArgs) Handles mnuEnable.Click
      Dim Password As String

      Password = InputBox("Enter the Administrators password.")
      If Password.ToUpper = "PASSWORD" Then
         mnuDataPath.Enabled = True
         mnuRobotDataPath.Enabled = True
         mnuEnable.Enabled = False
      End If
   End Sub

   Private Sub txbInterval_TextChanged(sender As Object, e As EventArgs)
      Try
         If IsInteger(txbInterval.Text, 1) Then
            Interval_min = CInt(txbInterval.Text)
            LogTime = Now.AddMinutes(Interval_min)
            My.Settings.LogInterval = Interval_min
            My.Settings.Save()
         End If
      Catch ex As Exception
      End Try
   End Sub

   Private Sub mnuExit_Click(sender As Object, e As EventArgs) Handles mnuExit.Click
      Me.Close()
   End Sub


   Private Sub cbxChartLength_SelectedIndexChanged(sender As Object, e As EventArgs)
      'Chart lenght is measured in weeks.

      If cbxChartLength.SelectedIndex = 0 Then   '1 week
         ChartLength = 1
      ElseIf cbxChartLength.SelectedIndex = 1 Then   '2 weeks
         ChartLength = 2
      ElseIf cbxChartLength.SelectedIndex = 2 Then    '1 month
         ChartLength = 4
      ElseIf cbxChartLength.SelectedIndex = 3 Then   '2 month
         ChartLength = 8
      ElseIf cbxChartLength.SelectedIndex = 4 Then   '3 month
         ChartLength = 13
      ElseIf cbxChartLength.SelectedIndex = 5 Then   '6 month
         ChartLength = 26
      ElseIf cbxChartLength.SelectedIndex = 6 Then   '9 month
         ChartLength = 39
      ElseIf cbxChartLength.SelectedIndex = 7 Then   '1 year
         ChartLength = 52
      ElseIf cbxChartLength.SelectedIndex = 8 Then   'Maximum.   Specified as 10 years
         ChartLength = 520
      End If
      RecallData()
   End Sub

End Class



Public Class HumidityMeter
   Dim WithEvents MyPort As New IO.Ports.SerialPort
   Public Humidity As Double
   Public Temperature As Double
   Public BarometricPressure As Double
   Private InTestMode As Boolean = False


   'The meter auto transmits it's data.   It is set up to transmit data every 2 seconds.
   Function Initialize() As Boolean
      Dim Success As Boolean

      MyPort.BaudRate = 9600
      MyPort.DataBits = 8
      MyPort.Parity = IO.Ports.Parity.None
      MyPort.NewLine = vbCr
      Success = OpenCommPort()

      Return Success
   End Function

   Public Property TestMode() As Boolean
      Get
         Return InTestMode
      End Get
      Set(value As Boolean)
         InTestMode = value
      End Set
   End Property

   Private Function OpenCommPort() As Boolean
      Dim Success As Boolean = False
      Dim Timeout As DateTime

      If InTestMode = True Then
         Return True
      End If

      If frmMain.TestMode = False Then
         Try
            MyPort.PortName = My.Settings.HumidityPort
            frmMain.lblComms.Text = "Opening communications.   Testing COMM port : " & MyPort.PortName
            frmMain.lblComms.Visible = True
            MyPort.Open()
            MyPort.DiscardInBuffer()
            Timeout = Now.AddSeconds(6.0)
            While MyPort.BytesToRead < 20 And DateTime.Compare(Now, Timeout) < 0
               Application.DoEvents()
            End While
            If MyPort.BytesToRead >= 20 Then
               Success = True
            Else
               MyPort.Close()
               Success = FindCommPort()
            End If
         Catch ex As Exception
            MyPort.Close()
            FindCommPort()
         End Try
      End If

      frmMain.lblComms.Visible = False
      Return Success
   End Function


   Private Function FindCommPort() As Boolean
      Dim Pass As Int32
      Dim Foundit As Boolean = False
      Dim Timeout As DateTime


      For Pass = 0 To My.Computer.Ports.SerialPortNames.Count - 1
         MyPort.PortName = My.Computer.Ports.SerialPortNames.Item(Pass)
         frmMain.lblComms.Text = "Opening communications.   Testing COMM port : " & MyPort.PortName
         If MyPort.IsOpen = False Then  'The real port can not be open
            Try
               MyPort.Open()
               MyPort.DiscardInBuffer()
               Timeout = Now.AddSeconds(6.0)
               While MyPort.BytesToRead < 20 And DateTime.Compare(Now, Timeout) < 0
                  Application.DoEvents()
               End While
               If MyPort.BytesToRead > 20 Then
                  Foundit = True
                  My.Settings.HumidityPort = MyPort.PortName
                  My.Settings.Save()
                  Foundit = True
                  Exit For
               Else
                  MyPort.Close()
               End If
            Catch ex As Exception
               MyPort.Close()
               If ex.Message = "User requested abort" Then
                  Throw
               End If
            End Try
         End If
      Next

      Return Foundit
   End Function



   Sub GetReadings()
      'The meter autonomously sends readings every 1 second, in this form
      '41040100000547[0x13][0x02]
      '42010100000232[0x13][0x02]
      '43780100006503[0x13][0x02]
      'I don't know what the format means, but the first 2 chars and last 4 chars are important.  The first 2 chars indicate the type of reading and the last 4 chars are the value.
      '41 xxxx xxxx 0547 is humidity of 54.7
      '42 xxxx xxxx 0232 is temperature of 23.2
      '43 xxxx xxxx 6503 is barometric pressure of 650.3 mmHg
      'Each string is 16 charactors.   If you wait until the inbuffer has 64 charactors you are garenteed to have all three values.
      Dim Input As String
      Dim Pos As Int32
      Dim Value As String
      Dim Points As Int32

      If InTestMode = True Then
         Humidity = 75.0
         Temperature = 23.0
         BarometricPressure = 650.0
         Exit Sub
      End If

      Do
         Application.DoEvents()
      Loop While MyPort.BytesToRead < 65
      Input = MyPort.ReadExisting()
      Try
         Points = 0
         Input = Input.Substring(Input.Length - 65)  'Get just the last 64 bytes

         For Pos = 0 To 64
            If Input.Chars(Pos) = vbCr Then
               Value = Input.Substring(Pos + 12, 4)

               If Input.Chars(Pos + 3) = "1" Then   'This is the "1" of 41, which is humidity
                  Humidity = CDbl(Value) / 10.0
                  Points += 1
               ElseIf Input.Chars(Pos + 3) = "2" Then   'This is the "2" of 42, which is temperature
                  Temperature = CDbl(Value) / 10.0
                  Points += 1
               ElseIf Input.Chars(Pos + 3) = "3" Then   'This is the "3" of 43, which is barometric pressure
                  BarometricPressure = CDbl(Value) / 10.0
                  Points += 1
               End If
               If Points = 3 Then
                  Exit For
               End If
               Pos = Pos + 15    'Move to the char just before the next Cr
            End If
         Next
         MyPort.DiscardInBuffer()
      Catch ex As Exception
      End Try
   End Sub

End Class