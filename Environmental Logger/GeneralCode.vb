Imports System.Environment
Imports System.IO


'Routeins in here include
'**********************************
'   Public Sub CreateDataFileFolders(ByRef path As String)
'Function GetTime(ByRef myTextBox As TextBox) As myTimeReply
'Function FormatTime(ByVal myTime As TimeSpan, ByVal withDays As Boolean, ByVal withHours As Boolean, ByVal withMinutes As Boolean, ByVal withSeconds As Boolean) As String
'Function IsInteger(ByRef myString As String, Optional ByVal minimum As Int32 = Int32.MinValue, Optional ByVal maximum As Int32 = Int32.MaxValue) As Boolean
'Function IsDouble(ByRef myString As String, Optional ByVal minimum As Double = Double.MinValue, Optional ByVal maximum As Double = Double.MaxValue) As Boolean
'Sub DelayMS(ByVal Delay As Int32)
'Sub DelaySeconds(ByVal Delay As int32)
'Sub DelaySeconds(ByVal Delay As TimeSpan)
'Sub HomeAll()
'Function FindMaximum(ByRef NumArray() As Int32) As Int32
'Function FindMaximum(ByRef NumArray() As Single) As Single
'Function FindMaximum(ByRef NumArray() As Double) As Double
'Function FindMinimum(ByRef NumArray() As Int32) As Int32
'Function FindMinimum(ByRef NumArray() As Single) As Single
'Function FindMinimum(ByRef NumArray() As Double) As Double
'Sub CountDownTimer(byref myTextBox as TextBox)
'Function GetAcsiiCode(ByVal keyValue As Int32, ByVal shiftSet As Boolean) As Int32
'
'Microtiter plate functions
'Function ReturnCellBy8(ByVal vial As Int32) As String

'Classes
'Class PaintRacks


Module GeneralCode

   ''' <summary>
   ''' If the application data folder does not exist it creates it and copies all files in the application startup path with the extension of '.kem' to the data folder.
   ''' </summary>
   ''' <remarks></remarks>
   Public Sub CreateDataFileFolders(ByRef path As String)
      'If this is the first time the project has been started, then the path will not exist.
      'This section copies any file with the extension of .kem from teh appliaction startup folder to the new data folder.   In this way
      'the project can copy all read/write files to the application startup path on installation, and then copy them to a special folder 
      'Where the program can read and write to them.
      If My.Computer.FileSystem.DirectoryExists(path) = False Then
         My.Computer.FileSystem.CreateDirectory(path)
      End If
   End Sub



   ''' <summary>
   ''' Tests all unopened comm ports to identify the port of interest.
   ''' </summary>
   ''' <param name="testCommand">Command to seen to the instrument, including all terminating charactors.</param>
   ''' <param name="expectedReply">Expected reply.</param>
   '''  <param name="terminatingCharactor">Terminating charactor for the reply.</param>
   ''' <returns>Comm port name.</returns>
   ''' <remarks></remarks>
   Public Function SearchCommPorts(ByVal testCommand As String, ByVal expectedReply As String, ByVal terminatingCharactor As String, Optional ByVal readTimeout As Int32 = 1000) As String
      Dim Name As String = ""
      Dim PortNames() As String
      Dim MyPort As New System.IO.Ports.SerialPort
      Dim Reply As String


      PortNames = System.IO.Ports.SerialPort.GetPortNames
      For Pass As Int32 = 0 To PortNames.Length - 1
         MyPort.PortName = PortNames(Pass)
         If MyPort.IsOpen = False Then
            MyPort.ReadTimeout = readTimeout
            MyPort.NewLine = terminatingCharactor
            Try
               MyPort.Open()
               Try
                  MyPort.DiscardInBuffer()
                  MyPort.DiscardOutBuffer()
                  MyPort.Write(testCommand)
                  Reply = MyPort.ReadLine
                  If Reply.Contains(expectedReply) Then
                     Name = MyPort.PortName
                     Exit For
                  Else
                     MyPort.DiscardInBuffer()
                     MyPort.DiscardOutBuffer()
                     MyPort.Write(testCommand)
                     Reply = MyPort.ReadLine
                     If Reply.Contains(expectedReply) Then
                        Name = MyPort.PortName
                        Exit For
                     End If
                  End If
               Catch ex As Exception
                  If ex.Message.Contains("User requested abort") Then
                     Throw
                  End If
               End Try
            Catch ex As Exception
               If ex.Message.Contains("User requested abort") Then
                  Throw
               End If
            End Try
         End If
         MyPort.Close()
      Next

      MyPort.Close()
      Return Name
   End Function

   Public Function GetTime(ByRef myTextBox As TextBox) As Int32
      'This function looks at the supplied text box to see if time is entered in the format
      'of xxx:xx:xx.  If the format is incorrect, the function clears the textbox and returns
      'a value of int32.minval
      'Requirements:
      'The input must have 2 ":" or ";"
      'The seconds and minutes groups can have 0-2 digits.
      Dim returnValue As Int32 = 0
      Dim mychar(1) As Char
      Dim mystrings() As String
      Dim seconds As Int32
      Dim minutes As Int32
      Dim hours As Int32

      Try
         'Varify the input format is OK
         mychar(0) = CType(":", Char)
         mychar(1) = CType(";", Char)
         mystrings = myTextBox.Text.Split(mychar)
         If mystrings.GetUpperBound(0) = 2 Then  'Three substrings were created
            If mystrings(2) <> "" Then  'holds the seconds
               If IsInteger(mystrings(2)) = False Or mystrings(2).Length > 2 Then
                  returnValue = Int32.MinValue
               End If
            End If
            If mystrings(1) <> "" Then  'holds the minutes
               If IsInteger(mystrings(1)) = False Or mystrings(1).Length > 2 Then
                  returnValue = Int32.MinValue
               End If
            End If
            If mystrings(0) <> "" Then  'holds the hours
               If IsInteger(mystrings(0)) = False Then
                  returnValue = Int32.MinValue
               End If
            End If
         Else
            returnValue = Int32.MinValue
         End If

         If returnValue = 0 Then
            If mystrings(2) <> "" Then
               seconds = CType(mystrings(2), Int32)
            End If
            If mystrings(1) <> "" Then
               minutes = CType(mystrings(1), Int32)
               seconds = seconds + minutes * 60
            End If
            If mystrings(0) <> "" Then
               hours = CType(mystrings(0), Int32)
               seconds = seconds + hours * 3600
            End If
            returnValue = seconds
         End If

      Catch ex As Exception
         If ex.Message.Contains("User requested abort") Then
            Throw
         End If
         returnValue = Int32.MinValue
      End Try

      If returnValue = Int32.MinValue Then
         myTextBox.Text = "00:00:00"
         MessageBox.Show("Error in time format.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
      End If

      Return returnValue
   End Function


   Public Function GetTime(ByVal text As String) As Int32
      'Returns the time represented in the string as seconds
      'This function looks at the supplied string to see if time is entered in the format
      'of xxx:xx:xx.  If the format is incorrect, the function returns a value of 0
      'Requirements:
      'The input must have 2 ":" or ";"
      'The seconds and minutes groups can have 0-2 digits.
      Dim returnValue As Int32 = 0
      Dim mychar(1) As Char
      Dim mystrings() As String
      Dim seconds As Int32
      Dim minutes As Int32
      Dim hours As Int32

      Try
         'Varify the input format is OK
         mychar(0) = CType(":", Char)
         mychar(1) = CType(";", Char)
         mystrings = text.Split(mychar)
         If mystrings.GetUpperBound(0) = 2 Then  'Three substrings were created
            If mystrings(2) <> "" Then  'holds the seconds
               If IsInteger(mystrings(2)) = False Or mystrings(2).Length > 2 Then
                  returnValue = Int32.MinValue
               End If
            End If
            If mystrings(1) <> "" Then  'holds the minutes
               If IsInteger(mystrings(1)) = False Or mystrings(1).Length > 2 Then
                  returnValue = Int32.MinValue
               End If
            End If
            If mystrings(0) <> "" Then  'holds the hours
               If IsInteger(mystrings(0)) = False Then
                  returnValue = Int32.MinValue
               End If
            End If
         Else
            returnValue = Int32.MinValue
         End If

         If returnValue = 0 Then
            If mystrings(2) <> "" Then
               seconds = CType(mystrings(2), Int32)
            End If
            If mystrings(1) <> "" Then
               minutes = CType(mystrings(1), Int32)
               seconds = seconds + minutes * 60
            End If
            If mystrings(0) <> "" Then
               hours = CType(mystrings(0), Int32)
               seconds = seconds + hours * 3600
            End If
            returnValue = seconds
         End If

      Catch ex As Exception
         If ex.Message.Contains("User requested abort") Then
            Throw
         End If
         returnValue = 0
      End Try

      Return returnValue
   End Function


   Public Function FormatTime(ByVal myTime As TimeSpan, ByVal withDays As Boolean, ByVal withHours As Boolean, ByVal withMinutes As Boolean, ByVal withSeconds As Boolean) As String

      'This function returns a string with the specified formatting
      Dim TimeString As String = ""

      If withDays = True Then
         TimeString = myTime.Days.ToString
         If withHours = True Then
            If TimeString = "" Then
               TimeString = Format(myTime.Hours, "0")
            Else
               TimeString = TimeString & ":" & Format(myTime.Hours, "00")
            End If
         End If
      Else
         If withHours = True Then
            If TimeString = "" Then
               TimeString = Format(((myTime.Days * 24) + myTime.Hours), "0")
            Else
               TimeString = TimeString & ":" & Format(myTime.Hours, "00")
            End If
         End If
      End If

      If withMinutes = True Then
         If TimeString = "" Then
            TimeString = Format(myTime.Minutes, "0")
         Else
            TimeString = TimeString & ":" & Format(myTime.Minutes, "00")
         End If
      End If
      If withSeconds = True Then
         If TimeString = "" Then
            TimeString = Format(myTime.Seconds, "0")
         Else
            If myTime.Milliseconds >= 500 Then
               TimeString = TimeString & ":" & Format((myTime.Seconds + 1), "00")
            Else
               TimeString = TimeString & ":" & Format(myTime.Seconds, "00")
            End If
         End If
      End If

      Return TimeString
   End Function


   Public Function FormatTime(ByVal seconds As Double, ByVal withDays As Boolean) As String
      Dim Time As TimeSpan
      Dim Reply As String

      Time = TimeSpan.FromSeconds(seconds)
      If withDays = True Then
         Reply = Time.Days.ToString & ":" & Format(Time.Hours, "00") & ":" & Format(Time.Minutes, "00") & ":" & Format(Time.Seconds, "00")
      Else
         Reply = ((Time.Days * 24.0) + Time.Hours).ToString & ":" & Format(Time.Minutes, "00") & ":" & Format(Time.Seconds, "00")
      End If

      Return Reply
   End Function


   Public Function IsInteger(ByRef myString As String, Optional ByVal minimum As Int32 = Int32.MinValue, Optional ByVal maximum As Int32 = Int32.MaxValue, Optional ByVal number As Int32 = Nothing) As Boolean
      'This function examines the string and returns true if the string contains nothing
      'except digits and is between the min and max values specified
      Dim answer As Boolean = True
      Dim position As Int32

      Try
         If myString <> "" Then
            For position = 0 To myString.Length - 1
               answer = Char.IsDigit(myString.Chars(position))
               If answer = False Then
                  If Not (position = 0 And myString.Chars(0) = "-") Then
                     Return False
                  End If
               End If
            Next
            position = CType(myString, Int32)
            If position < minimum Or position > maximum Then
               answer = False
            End If
         Else
            answer = False
         End If
         If Not number = Nothing Then
            number = position
         End If

      Catch ex As Exception
         If ex.Message.Contains("User requested abort") Then
            Throw
         End If
         Return False
      End Try

      Return answer
   End Function


   Public Function IsInteger(ByRef value As Object, Optional ByVal minimum As Int32 = Int32.MinValue, Optional ByVal maximum As Int32 = Int32.MaxValue) As Boolean
      'This function examines the string and returns true if the string contains nothing
      'except digits and is between the min and max values specified
      Dim Answer As Boolean = True
      Dim Number As Int32

      Try
         If value Is Nothing Then
            Answer = False
         Else
            Try
               number = CInt(value)
               If Number < minimum Then
                  Answer = False
               ElseIf Number > maximum Then
                  Answer = False
               End If
            Catch ex As Exception
               If ex.Message.Contains("User requested abort") Then
                  Throw
               End If
               Answer = False
            End Try
         End If
      Catch ex As Exception
         If ex.Message.Contains("User requested abort") Then
            Throw
         End If
         Answer = False
      End Try

      Return Answer
   End Function

   Public Function IsDouble(ByRef myString As String, Optional ByVal minimum As Double = Double.MinValue, Optional ByVal maximum As Double = Double.MaxValue) As Boolean
      'This function examines the string and returns true if the string contains nothing
      'except digits and is between the min and max for an a Double
      Dim answer As Boolean = True
      Dim decimalFound As Boolean = False
      Dim position As Int32
      Dim value As Double

      Try
         If myString <> "" Then
            For position = 0 To myString.Length - 1
               If Char.IsDigit(myString.Chars(position)) Or _
                  (position = 0 And myString.Chars(0) = "-") Or _
                  (myString.Chars(position) = "." And decimalFound = False) Then
                  If myString.Chars(position) = "." Then
                     decimalFound = True
                  End If
               Else
                  Return False
               End If
            Next
            value = CType(myString, Double)
            If value < minimum Or value > maximum Then
               answer = False
            End If
         Else
            answer = False
         End If

      Catch ex As Exception
         If ex.Message.Contains("User requested abort") Then
            Throw
         End If
         Return False
      End Try

      Return answer
   End Function

   Public Function IsDouble(ByRef myObj As Object, Optional ByVal minimum As Double = Double.MinValue, Optional ByVal maximum As Double = Double.MaxValue) As Boolean
      'This function examines the string and returns true if the string contains nothing
      'except digits and is between the min and max for an a Double
      Dim answer As Boolean = True
      Dim value As Double


      Try
         value = CDbl(myObj)
         If value < minimum Or value > maximum Then
            answer = False
         End If
      Catch ex As Exception
         If ex.Message.Contains("User requested abort") Then
            Throw
         End If
         Return False
      End Try

      Return answer
   End Function


   Public Function RoundUp(ByVal number As Double) As Int32
      Dim WholePortion As String
      Dim FractionalPortion As String
      Dim Test As Double
      Dim Answer As Int32

      If number.ToString.Contains(".") Then
         WholePortion = number.ToString.Substring(0, number.ToString.IndexOf("."))
         FractionalPortion = number.ToString.Substring(number.ToString.IndexOf("."))
         Test = CDbl(FractionalPortion)
         If Test > 0.0 Then
            Answer = CInt(WholePortion) + 1
         Else
            Answer = CInt(WholePortion)
         End If
      Else
         Answer = CInt(number)
      End If

      Return Answer
   End Function

   Public Function RoundDown(ByVal number As Double) As Int32
      Dim WholePortion As String
      Dim Answer As Int32

      If number.ToString.Contains(".") Then
         WholePortion = number.ToString.Substring(0, number.ToString.IndexOf("."))
         Answer = CInt(WholePortion)
      Else
         Answer = CInt(number)
      End If

      Return Answer
   End Function


   ''' <summary>
   ''' Returns a string used in filenames, like '10_12_17 at 12:56:23'
   ''' </summary>
   ''' <returns>File time string.</returns>
   ''' <remarks></remarks>
   Public Function GetFileTimeString() As String
      Dim Data As String

      Data = Now.Month.ToString & "_" & Now.Day.ToString & "_" & Now.Year.ToString & " at " & Now.Hour.ToString & ":" & Now.Minute.ToString & ":" & Now.Second.ToString
      Return Data
   End Function


   Public Sub DelayMS(ByVal Delay As Double)
      '***********************************************************************
      Dim EndTime As DateTime

      EndTime = Now.AddMilliseconds(Delay)
      While DateTime.Compare(Now, EndTime) < 0.0
         Application.DoEvents()
      End While
   End Sub


   Public Sub DelaySeconds(ByVal Delay As Double)
      '***********************************************************************
      Dim EndTime As DateTime

      EndTime = Now.AddSeconds(Delay)
      While DateTime.Compare(Now, EndTime) < 0.0
         Application.DoEvents()
      End While
   End Sub


   Public Sub DelaySeconds(ByVal Delay As TimeSpan)
      '***********************************************************************
      Dim EndTime As DateTime

      EndTime = Now.Add(Delay)
      While DateTime.Compare(Now, EndTime) < 0.0
         Application.DoEvents()
      End While
   End Sub

   Public Function FindMinimum(ByRef NumArray() As Int32, Optional ByVal startElement As Int32 = 0, Optional ByVal endElement As Int32 = Int32.MaxValue) As Int32
      Dim Element As Int32
      Dim Min As Int32 = Int32.MaxValue

      If endElement = Int32.MaxValue Then
         endElement = NumArray.Length - 1
      End If

      For Element = startElement To endElement
         Try
            If NumArray(Element) < Min Then
               Min = NumArray(Element)
            End If
         Catch ex As Exception
            If ex.Message.Contains("User requested abort") Then
               Throw
            End If
         End Try
      Next

      Return Min
   End Function

   Public Function FindMinimum(ByRef NumArray() As Double, Optional ByVal startElement As Int32 = 0, Optional ByVal endElement As Int32 = Int32.MaxValue) As Double
      Dim Element As Int32
      Dim Min As Double = Double.MaxValue

      If endElement = Int32.MaxValue Then
         endElement = NumArray.Length - 1
      End If

      For Element = startElement To endElement
         Try
            If NumArray(Element) < Min Then
               Min = NumArray(Element)
            End If
         Catch ex As Exception
            If ex.Message.Contains("User requested abort") Then
               Throw
            End If
         End Try
      Next

      Return Min
   End Function

   Public Function FindMaximum(ByRef NumArray() As Int32, Optional ByVal startElement As Int32 = 0, Optional ByVal endElement As Int32 = Int32.MaxValue) As Int32
      Dim Element As Int32
      Dim Max As Int32 = Int32.MinValue

      If endElement = Int32.MaxValue Then
         endElement = NumArray.Length - 1
      End If

      For Element = startElement To endElement
         Try
            If NumArray(Element) > Max Then
               Max = NumArray(Element)
            End If
         Catch ex As Exception
            If ex.Message.Contains("User requested abort") Then
               Throw
            End If
         End Try
      Next

      Return Max
   End Function

   Public Function FindMaximum(ByRef NumArray() As Double, Optional ByVal startElement As Int32 = 0, Optional ByVal endElement As Int32 = Int32.MaxValue) As Double
      Dim Element As Int32
      Dim Max As Double = Double.MinValue

      If endElement = Int32.MaxValue Then
         endElement = NumArray.Length - 1
      End If

      For Element = startElement To endElement
         Try
            If NumArray(Element) > Max Then
               Max = NumArray(Element)
            End If
         Catch ex As Exception
            If ex.Message.Contains("User requested abort") Then
               Throw
            End If
         End Try
      Next

      Return Max
   End Function

   ''' <summary>
   ''' Returns a value within the limits of min and max.
   ''' </summary>
   ''' <param name="input">The number to operate on</param>
   ''' <param name="min">The minimum allowed value</param>
   ''' <param name="max">The maximum allowed value</param>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Function EnforceLimits(ByVal input As Int32, ByVal min As Int32, ByVal max As Int32) As Int32
      If input < min Then
         input = min
      ElseIf input > max Then
         input = max
      End If

      Return input
   End Function


   ''' <summary>
   ''' Returns a value within the limits of min and max.
   ''' </summary>
   ''' <param name="input">The number to operate on</param>
   ''' <param name="min">The minimum allowed value</param>
   ''' <param name="max">The maximum allowed value</param>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Function EnforceLimits(ByVal input As Double, ByVal min As Double, ByVal max As Double) As Double
      If input < min Then
         input = min
      ElseIf input > max Then
         input = max
      End If

      Return input
   End Function


   ''' <summary>
   ''' Extracts the first numeric value it finds in a string including decimal point and decimals
   ''' </summary>
   ''' <param name="myString">String to extact the number from</param>
   ''' <returns>The extracted number as a string</returns>
   ''' <remarks></remarks>
   Function ExtractNumberFromString(ByRef myString As String) As String
      Dim MyChars() As Char
      Dim Pass As Int32
      Dim FoundNumbers As Boolean
      Dim FoundDecimal As Boolean
      Dim FoundSign As Boolean
      Dim MyReturn As String = ""


      MyChars = myString.ToCharArray
      For Pass = 0 To MyChars.Length - 1
         If Asc(MyChars(Pass)) >= 48 And Asc(MyChars(Pass)) <= 57 Then   'numbers 
            FoundNumbers = True
            MyReturn += MyChars(Pass)

         ElseIf Asc(MyChars(Pass)) = 46 Then 'Decimal point
            If FoundDecimal = True Then
               Exit For    'A second decimal point is the end of input
            End If
            If FoundNumbers = True Then
               MyReturn += MyChars(Pass)
               FoundDecimal = True
            Else    'Is there a number immediatlely after the decimal?
               If Pass <= MyChars.Length - 2 Then
                  If Asc(MyChars(Pass + 1)) >= 48 And Asc(MyChars(Pass + 1)) <= 57 Then
                     MyReturn += MyChars(Pass)
                     FoundDecimal = True
                  End If
               End If
            End If

         ElseIf Asc(MyChars(Pass)) = 45 Then 'Negative sign
            If FoundSign = True Then
               Exit For    'cant have 2 signs
            End If
            'Two situations are allowed.  1) the next char is a digit 2) the next char is a decimal followed by a digit
            If Pass <= MyChars.Length - 2 Then
               If Asc(MyChars(Pass + 1)) >= 48 And Asc(MyChars(Pass + 1)) <= 57 Then
                  MyReturn += MyChars(Pass)
                  FoundSign = True
               End If
            End If
            If Pass <= MyChars.Length - 3 And Asc(MyChars(Pass + 1)) = 46 Then
               If Asc(MyChars(Pass + 2)) >= 48 And Asc(MyChars(Pass + 2)) <= 57 Then
                  MyReturn += MyChars(Pass)
                  FoundSign = True
               End If
            End If
         Else
            If FoundNumbers = True Then
               Exit For
            End If
         End If
      Next

      Return MyReturn
   End Function


   Function GetColor(ByVal C As Int32, ByVal M As Int32, ByVal Y As Int32, ByVal K As Int32) As System.Drawing.Color
      Dim R As Int32
      Dim G As Int32
      Dim B As Int32


      R = CInt(255.0 * (1.0 - (C / 100)) * (1.0 - (K / 100)))
      G = CInt(255.0 * (1.0 - (M / 100)) * (1.0 - (K / 100)))
      B = CInt(255.0 * (1.0 - (Y / 100)) * (1.0 - (K / 100)))

      Return System.Drawing.Color.FromArgb(CType(CType(R, Byte), Integer), CType(CType(G, Byte), Integer), CType(CType(B, Byte), Integer))
   End Function


   Function GetColor(ByVal R As Int32, ByVal G As Int32, ByVal B As Int32) As System.Drawing.Color
      Return System.Drawing.Color.FromArgb(CType(CType(R, Byte), Integer), CType(CType(G, Byte), Integer), CType(CType(B, Byte), Integer))
   End Function

#Region "Microtiter Plate Functions"

   Function CellName_96_By8(ByVal vial As Int32) As String
      'For use with 96 position plates
      'Returns the cell number of the titer plate when the plate is filled by columns of 8 and then advances to the 
      'next column.  A plate is numbered as cell 1= A1, cell 2= B1
      Dim Cell As String
      Dim Value As Int32

      Value = ((vial - 1) Mod 8) + 1
      Cell = Chr(Value + 64)  'Capital A has an aasci value of 65
      Value = ((vial - 1) \ 8) + 1
      Cell = Cell & Value.ToString

      Return Cell
   End Function


   Function CellNumber_96_by8(ByVal cell As String) As Int32
      'For use with 96 position plates
      'This sub returns the cell number (1-96) when it is passed the cell name such as B5.
      'Use this function when the plate is filled by groups of 8.  1=A1,  5=E1
      'This function returns -1 for an invalid cell
      Dim Letter As Char
      Dim LetterValue As Int32
      Dim Number As String
      Dim NumberValue As Int32

      Letter = CType(cell.Substring(0, 1), Char)
      Number = cell.Substring(1, cell.Length - 1)
      NumberValue = (CType(Number, Int32) - 1) * 8
      If NumberValue < 0 Or NumberValue > 88 Then
         Return -1
      End If
      LetterValue = AscW(Letter) - 64   'The aasci value of capital A is 65
      If LetterValue < 1 Or LetterValue > 8 Then
         Return -1
      End If
      NumberValue = NumberValue + LetterValue

      Return NumberValue
   End Function


   Function CellName_96_By12(ByVal vial As Int32) As String
      'For use with 96 position plates
      'Returns the cell number of the titer plate when the plate is filled by columns of 12 and then advances to the 
      'next column.  A plate is numbered as cell 1= A1, cell 2= A2
      Dim Cell As String
      Dim Value As Int32

      Value = ((vial - 1) \ 12) + 1
      Cell = Chr(Value + 64)
      Value = ((vial - 1) Mod 12) + 1
      Cell = Cell & Value.ToString

      Return Cell
   End Function

   Function CellNumber_96_by12(ByVal cell As String) As Int32
      'For use with 96 position plates
      'This sub returns the cell number (1-96) when it is passed the cell name such as B5.
      'Use this function when the plate is filled by groups of 12.  1=A1,  5=A5
      'This function returns -1 for an invalid cell
      Dim Letter As Char
      Dim LetterValue As Int32
      Dim Number As String
      Dim NumberValue As Int32

      Letter = CType(cell.Substring(0, 1), Char)
      Number = cell.Substring(1, cell.Length - 1)
      LetterValue = (AscW(Letter) - 65) * 12
      If LetterValue < 0 Or LetterValue > 84 Then
         Return -1
      End If
      NumberValue = CType(Number, Int32)
      If NumberValue < 1 Or NumberValue > 12 Then
         Return -1
      End If
      NumberValue = NumberValue + LetterValue

      Return NumberValue
   End Function

   Function CellName_384_By16(ByVal vial As Int32) As String
      'For use with 384 cell plates.
      'Returns the cell number of the titer plate when the plate is filled by columns of 16 and then advances to the 
      'next column.  A plate is numbered as cell 1= A1, cell 5= E1
      Dim Cell As String
      Dim Value As Int32

      Value = ((vial - 1) Mod 16) + 1
      Cell = Chr(Value + 64)    'Capital A has an aasci value of 65
      Value = ((vial - 1) \ 16) + 1
      Cell = Cell & Value.ToString

      Return Cell
   End Function

   Function CellNumber_384_by16(ByVal cell As String) As Int32
      'For use with 384 cell plates.
      'This sub returns the cell number (1-384) when it is passed the cell name such as B5.
      'Use this function when the plate is filled by groups of 16.  1=A1,  5=E1
      'This function returns -1 for an invalid cell
      Dim Letter As Char
      Dim LetterValue As Int32
      Dim Number As String
      Dim NumberValue As Int32

      Letter = CType(cell.Substring(0, 1), Char)
      Number = cell.Substring(1, cell.Length - 1)
      NumberValue = (CType(Number, Int32) - 1) * 16
      If NumberValue < 0 Or NumberValue > 368 Then
         Return -1
      End If
      LetterValue = AscW(Letter) - 64   'The aasci value of capital A is 65
      If LetterValue < 1 Or LetterValue > 16 Then
         Return -1
      End If
      NumberValue = NumberValue + LetterValue

      Return NumberValue
   End Function


   Function CellName_384_By24(ByVal vial As Int32) As String
      'For use with 384 cell plates.
      'Returns the cell name of the titer plate when the plate is filled by columns of 24 and then advances to the 
      'next column.  A plate is numbered as cell 1= A1, cell 5= A5
      Dim Cell As String
      Dim Value As Int32

      Value = ((vial - 1) \ 24) + 1
      Cell = Chr(Value + 64)  'Aasci value of capital A is 65
      Value = ((vial - 1) Mod 24) + 1
      Cell = Cell & Value.ToString
      Return Cell
   End Function

   Function CellNumber_384_by24(ByVal cell As String) As Int32
      'For use with 384 cell plates.
      'This sub returns the cell number (1-384) when it is passed the cell name such as B5.
      'Use this function when the plate is filled by groups of 24.  1=A1,  5=A5
      'This function returns -1 for an invalid cell
      Dim Letter As Char
      Dim LetterValue As Int32
      Dim Number As String
      Dim NumberValue As Int32

      Letter = CType(cell.Substring(0, 1), Char)
      Number = cell.Substring(1, cell.Length - 1)
      LetterValue = (AscW(Letter) - 65) * 24
      If LetterValue < 0 Or LetterValue > 360 Then
         Return -1
      End If
      NumberValue = CType(Number, Int32)
      If NumberValue < 1 Or NumberValue > 24 Then
         Return -1
      End If
      NumberValue = NumberValue + LetterValue

      Return NumberValue
   End Function

#End Region


#Region "BIT manupulation functions"

   ''' <summary>
   ''' Clears the specifed bit in the value passed.
   ''' </summary>
   ''' <param name="data">Value to operate on.</param>
   ''' <param name="bit">Bit to clear.  Zero based.</param>
   ''' <returns>Process data value.</returns>
   ''' <remarks></remarks>
   Function ClearBit(ByVal data As Int32, ByVal bit As Int32) As Int32
      Dim BitMask As Int32 = 1

      BitMask = BitMask << bit
      data = data And Not BitMask
      Return data
   End Function

   Function ClearBit(ByVal data As Int16, ByVal bit As Int32) As Int16
      Dim BitMask As Int16 = 1

      BitMask = BitMask << bit
      data = data And Not BitMask
      Return data
   End Function

   Function ClearBit(ByVal data As Char, ByVal bit As Int32) As Char
      Dim BitMask As Int16 = 1
      Dim Value As Int32

      BitMask = BitMask << bit
      Value = Asc(data) And Not BitMask
      Return Chr(Value)
   End Function

   Function ClearBit(ByVal data As Byte, ByVal bit As Int32) As Byte
      Dim BitMask As Byte = 1

      BitMask = BitMask << bit
      data = data And Not BitMask
      Return data
   End Function

   ''' <summary>
   ''' Examines the specified bit of an integer number
   ''' </summary>
   ''' <param name="data">Integer to examine</param>
   ''' <param name="bit">0 based bit to examine</param>
   ''' <returns>True if bit is 1, False if 0</returns>
   ''' <remarks></remarks>
   Function ExamineBit(ByVal data As Int32, ByVal bit As Int32) As Boolean
      Dim BitMask As Int32 = 1

      BitMask = BitMask << bit
      If (data And BitMask) > 0 Then
         Return True
      Else
         Return False
      End If
   End Function

   Function ExamineBit(ByVal data As Int16, ByVal bit As Int32) As Boolean
      Dim BitMask As Int16 = 1

      BitMask = BitMask << bit
      If (data And BitMask) > 0 Then
         Return True
      Else
         Return False
      End If
   End Function

   Function ExamineBit(ByVal data As Char, ByVal bit As Int32) As Boolean
      Dim BitMask As Int16 = 1

      BitMask = BitMask << bit
      If (Asc(data) And BitMask) > 0 Then
         Return True
      Else
         Return False
      End If
   End Function

   Function ExamineBit(ByVal data As Byte, ByVal bit As Int32) As Boolean
      Dim BitMask As Byte = 1

      BitMask = BitMask << bit
      If (data And BitMask) > 0 Then
         Return True
      Else
         Return False
      End If
   End Function


   ''' <summary>
   ''' Sets the specifed bit to 1.
   ''' </summary>
   ''' <param name="data">Data item to operate on.</param>
   ''' <param name="bit">Bit position to set.  Zero based.</param>
   ''' <returns>The modified data item.</returns>
   ''' <remarks></remarks>
   Function SetBit(ByVal data As Int32, ByVal bit As Int32) As Int32
      Dim BitMask As Int32 = 1

      BitMask = BitMask << bit
      data = data Or BitMask
      Return data
   End Function

   Function SetBit(ByVal data As Int16, ByVal bit As Int32) As Int16
      Dim BitMask As Int16 = 1

      BitMask = BitMask << bit
      data = data Or BitMask
      Return data
   End Function

   Function SetBit(ByVal data As Char, ByVal bit As Int32) As Char
      Dim BitMask As Int16 = 1
      Dim Value As Int32

      BitMask = BitMask << bit
      Value = Asc(data) Or BitMask
      Return Chr(Value)
   End Function

   Function SetBit(ByVal data As Byte, ByVal bit As Int32) As Byte
      Dim BitMask As Byte = 1

      BitMask = BitMask << bit
      data = data Or BitMask
      Return data
   End Function


   ''' <summary>
   ''' Toggles the state of the specifed bit in the value passed to the function.
   ''' </summary>
   ''' <param name="data">Data item to operate on.</param>
   ''' <param name="bit">Bit to toggle.  Zero based.</param>
   ''' <returns>The modifed data item.</returns>
   ''' <remarks></remarks>
   Function ToggleBit(ByVal data As Int32, ByVal bit As Int32) As Int32
      Dim BitMask As Int32 = 1

      BitMask = BitMask << bit
      data = data Xor BitMask
      Return data
   End Function

   Function ToggleBit(ByVal data As Int16, ByVal bit As Int32) As Int16
      Dim BitMask As Int16 = 1

      BitMask = BitMask << bit
      data = data Xor BitMask
      Return data
   End Function

   Function ToggleBit(ByVal data As Char, ByVal bit As Int32) As Char
      Dim BitMask As Int16 = 1
      Dim Value As Int32

      BitMask = BitMask << bit
      Value = Asc(data) Xor BitMask
      Return Chr(Value)
   End Function

   Function ToggleBit(ByVal data As Byte, ByVal bit As Int32) As Byte
      Dim BitMask As Byte = 1

      BitMask = BitMask << bit
      data = data Xor BitMask
      Return data
   End Function

#End Region



#Region "ASCII Code Functions"

   ''' <summary>
   ''' Returns the ACSII code for the keyboard key pressed.  Implemented for text keys, number keys, symbol keys.
   ''' </summary>
   ''' <param name="keyValue">e.keyvalue of the key pressed.</param>
   ''' <param name="shiftSet">State of the ShiftKey.  Does not check for Cap Lock</param>
   ''' <returns>Ascii value of the key.   0 if the key is not defined.</returns>
   ''' <remarks></remarks>
   Public Function GetAcsiiCode(ByVal keyValue As Int32, ByVal shiftSet As Boolean) As Int32
      Dim AsciiValue As Int32 = 0

      If keyValue >= 48 And keyValue <= 57 Then   'These are the 0 to 9 keys
         If shiftSet = False Then
            AsciiValue = keyValue
         Else
            Select Case keyValue
               Case 48  'shift 0
                  AsciiValue = 41
               Case 49  'shift 1
                  AsciiValue = 33
               Case 50  'shift 2
                  AsciiValue = 64
               Case 51  'shift 3
                  AsciiValue = 35
               Case 52  'shift 4
                  AsciiValue = 36
               Case 53  'shift 5
                  AsciiValue = 37
               Case 54  'shift 6
                  AsciiValue = 94
               Case 55  'shift 7
                  AsciiValue = 38
               Case 56  'shift 8
                  AsciiValue = 42
               Case 57  'shift 9
                  AsciiValue = 40
            End Select
         End If

      ElseIf keyValue >= 65 And keyValue <= 90 Then   'Text keys A to Z
         If shiftSet = False Then
            AsciiValue = keyValue + 32
         Else
            AsciiValue = keyValue
         End If

      Else
         Select Case keyValue
            Case 192
               If shiftSet = False Then
                  AsciiValue = 96
               Else
                  AsciiValue = 126
               End If
            Case 189
               If shiftSet = False Then
                  AsciiValue = 45
               Else
                  AsciiValue = 95
               End If
            Case 187
               If shiftSet = False Then
                  AsciiValue = 61
               Else
                  AsciiValue = 43
               End If
            Case 219
               If shiftSet = False Then
                  AsciiValue = 91
               Else
                  AsciiValue = 123
               End If
            Case 221
               If shiftSet = False Then
                  AsciiValue = 93
               Else
                  AsciiValue = 125
               End If
            Case 220
               If shiftSet = False Then
                  AsciiValue = 92
               Else
                  AsciiValue = 124
               End If
            Case 186
               If shiftSet = False Then
                  AsciiValue = 59
               Else
                  AsciiValue = 58
               End If
            Case 227
               If shiftSet = False Then
                  AsciiValue = 39
               Else
                  AsciiValue = 34
               End If
            Case 188
               If shiftSet = False Then
                  AsciiValue = 44
               Else
                  AsciiValue = 60
               End If
            Case 190
               If shiftSet = False Then
                  AsciiValue = 46
               Else
                  AsciiValue = 62
               End If
            Case 191
               If shiftSet = False Then
                  AsciiValue = 47
               Else
                  AsciiValue = 63
               End If
            Case 32  'Space bar
               AsciiValue = 32
         End Select
      End If

      Return AsciiValue
   End Function
#End Region

End Module

