<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
   Inherits System.Windows.Forms.Form

   'Form overrides dispose to clean up the component list.
   <System.Diagnostics.DebuggerNonUserCode()> _
   Protected Overrides Sub Dispose(ByVal disposing As Boolean)
      Try
         If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
         End If
      Finally
         MyBase.Dispose(disposing)
      End Try
   End Sub

   'Required by the Windows Form Designer
   Private components As System.ComponentModel.IContainer

   'NOTE: The following procedure is required by the Windows Form Designer
   'It can be modified using the Windows Form Designer.  
   'Do not modify it using the code editor.
   <System.Diagnostics.DebuggerStepThrough()> _
   Private Sub InitializeComponent()
      Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
      Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
      Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
      Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
      Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
      Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
      Me.mnuExit = New System.Windows.Forms.ToolStripMenuItem()
      Me.ApplicationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
      Me.mnuEnable = New System.Windows.Forms.ToolStripMenuItem()
      Me.mnuDataPath = New System.Windows.Forms.ToolStripMenuItem()
      Me.mnuRobotDataPath = New System.Windows.Forms.ToolStripMenuItem()
      Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
      Me.Label1 = New System.Windows.Forms.Label()
      Me.txbInterval = New System.Windows.Forms.TextBox()
      Me.Label2 = New System.Windows.Forms.Label()
      Me.Label4 = New System.Windows.Forms.Label()
      Me.txbDirectory = New System.Windows.Forms.TextBox()
      Me.gbxInfo = New System.Windows.Forms.GroupBox()
      Me.btnLocate = New System.Windows.Forms.Button()
      Me.lblMessage = New System.Windows.Forms.Label()
      Me.lblComms = New System.Windows.Forms.Label()
      Me.Label3 = New System.Windows.Forms.Label()
      Me.Label5 = New System.Windows.Forms.Label()
      Me.lblTemperature = New System.Windows.Forms.Label()
      Me.lblHumidity = New System.Windows.Forms.Label()
      Me.cbxChartLength = New System.Windows.Forms.ComboBox()
      Me.Label6 = New System.Windows.Forms.Label()
      Me.MenuStrip1.SuspendLayout()
      CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.gbxInfo.SuspendLayout()
      Me.SuspendLayout()
      '
      'MenuStrip1
      '
      Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ApplicationToolStripMenuItem})
      Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
      Me.MenuStrip1.Name = "MenuStrip1"
      Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(9, 2, 0, 2)
      Me.MenuStrip1.Size = New System.Drawing.Size(1904, 24)
      Me.MenuStrip1.TabIndex = 0
      Me.MenuStrip1.Text = "MenuStrip1"
      '
      'FileToolStripMenuItem
      '
      Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuExit})
      Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
      Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
      Me.FileToolStripMenuItem.Text = "File"
      '
      'mnuExit
      '
      Me.mnuExit.Name = "mnuExit"
      Me.mnuExit.Size = New System.Drawing.Size(93, 22)
      Me.mnuExit.Text = "Exit"
      '
      'ApplicationToolStripMenuItem
      '
      Me.ApplicationToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuEnable, Me.mnuDataPath, Me.mnuRobotDataPath})
      Me.ApplicationToolStripMenuItem.Name = "ApplicationToolStripMenuItem"
      Me.ApplicationToolStripMenuItem.Size = New System.Drawing.Size(80, 20)
      Me.ApplicationToolStripMenuItem.Text = "Application"
      '
      'mnuEnable
      '
      Me.mnuEnable.Name = "mnuEnable"
      Me.mnuEnable.Size = New System.Drawing.Size(223, 22)
      Me.mnuEnable.Text = "Enable Application Menus"
      '
      'mnuDataPath
      '
      Me.mnuDataPath.Enabled = False
      Me.mnuDataPath.Name = "mnuDataPath"
      Me.mnuDataPath.Size = New System.Drawing.Size(223, 22)
      Me.mnuDataPath.Text = "Humidity Data File Directory"
      '
      'mnuRobotDataPath
      '
      Me.mnuRobotDataPath.Enabled = False
      Me.mnuRobotDataPath.Name = "mnuRobotDataPath"
      Me.mnuRobotDataPath.Size = New System.Drawing.Size(223, 22)
      Me.mnuRobotDataPath.Text = "Robot Data Directory"
      '
      'Chart1
      '
      ChartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.DeepSkyBlue
      ChartArea1.AxisX.MajorTickMark.LineWidth = 2
      ChartArea1.AxisX.MinorGrid.Enabled = True
      ChartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.PaleTurquoise
      ChartArea1.AxisX.Title = "Time"
      ChartArea1.AxisX.TitleFont = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      ChartArea1.AxisY.LabelStyle.Format = "F1"
      ChartArea1.AxisY.LineColor = System.Drawing.Color.Red
      ChartArea1.AxisY.LineWidth = 2
      ChartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.DarkGray
      ChartArea1.AxisY.Title = "Temperature (oC)"
      ChartArea1.AxisY.TitleFont = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      ChartArea1.AxisY2.LabelStyle.Format = "F1"
      ChartArea1.AxisY2.LineColor = System.Drawing.Color.Blue
      ChartArea1.AxisY2.LineWidth = 2
      ChartArea1.AxisY2.MajorGrid.LineColor = System.Drawing.Color.LightSteelBlue
      ChartArea1.AxisY2.Title = "Humidity (%)"
      ChartArea1.AxisY2.TitleFont = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      ChartArea1.InnerPlotPosition.Auto = False
      ChartArea1.InnerPlotPosition.Height = 91.25531!
      ChartArea1.InnerPlotPosition.Width = 85.0!
      ChartArea1.InnerPlotPosition.X = 5.0!
      ChartArea1.InnerPlotPosition.Y = 1.11702!
      ChartArea1.Name = "ChartArea1"
      Me.Chart1.ChartAreas.Add(ChartArea1)
      Legend1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
      Legend1.IsTextAutoFit = False
      Legend1.Name = "Legend1"
      Legend1.Position.Auto = False
      Legend1.Position.Height = 6.229144!
      Legend1.Position.Width = 10.0!
      Legend1.Position.X = 90.0!
      Legend1.Position.Y = 3.0!
      Me.Chart1.Legends.Add(Legend1)
      Me.Chart1.Location = New System.Drawing.Point(11, 93)
      Me.Chart1.Margin = New System.Windows.Forms.Padding(4)
      Me.Chart1.Name = "Chart1"
      Series1.ChartArea = "ChartArea1"
      Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
      Series1.Color = System.Drawing.Color.Red
      Series1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Series1.Legend = "Legend1"
      Series1.Name = "Temperature"
      Series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime
      Series2.ChartArea = "ChartArea1"
      Series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
      Series2.Color = System.Drawing.Color.Blue
      Series2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Series2.Legend = "Legend1"
      Series2.Name = "Humidity"
      Series2.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary
      Me.Chart1.Series.Add(Series1)
      Me.Chart1.Series.Add(Series2)
      Me.Chart1.Size = New System.Drawing.Size(1876, 900)
      Me.Chart1.TabIndex = 1
      Me.Chart1.Text = "Chart1"
      '
      'Label1
      '
      Me.Label1.AutoSize = True
      Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.Label1.ForeColor = System.Drawing.Color.Blue
      Me.Label1.Location = New System.Drawing.Point(665, 34)
      Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
      Me.Label1.Name = "Label1"
      Me.Label1.Size = New System.Drawing.Size(94, 17)
      Me.Label1.TabIndex = 2
      Me.Label1.Text = "Log Interval"
      '
      'txbInterval
      '
      Me.txbInterval.Location = New System.Drawing.Point(771, 33)
      Me.txbInterval.Margin = New System.Windows.Forms.Padding(4)
      Me.txbInterval.Name = "txbInterval"
      Me.txbInterval.Size = New System.Drawing.Size(74, 23)
      Me.txbInterval.TabIndex = 3
      Me.txbInterval.Text = "30"
      '
      'Label2
      '
      Me.Label2.AutoSize = True
      Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.Label2.Location = New System.Drawing.Point(849, 37)
      Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
      Me.Label2.Name = "Label2"
      Me.Label2.Size = New System.Drawing.Size(57, 17)
      Me.Label2.TabIndex = 5
      Me.Label2.Text = "Minutes"
      '
      'Label4
      '
      Me.Label4.AutoSize = True
      Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.Label4.ForeColor = System.Drawing.Color.Blue
      Me.Label4.Location = New System.Drawing.Point(938, 53)
      Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
      Me.Label4.Name = "Label4"
      Me.Label4.Size = New System.Drawing.Size(125, 20)
      Me.Label4.TabIndex = 10
      Me.Label4.Text = "Data Directory"
      '
      'txbDirectory
      '
      Me.txbDirectory.Location = New System.Drawing.Point(1070, 53)
      Me.txbDirectory.Name = "txbDirectory"
      Me.txbDirectory.Size = New System.Drawing.Size(818, 23)
      Me.txbDirectory.TabIndex = 11
      '
      'gbxInfo
      '
      Me.gbxInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
      Me.gbxInfo.Controls.Add(Me.btnLocate)
      Me.gbxInfo.Controls.Add(Me.lblMessage)
      Me.gbxInfo.Location = New System.Drawing.Point(233, 105)
      Me.gbxInfo.Name = "gbxInfo"
      Me.gbxInfo.Size = New System.Drawing.Size(559, 148)
      Me.gbxInfo.TabIndex = 12
      Me.gbxInfo.TabStop = False
      Me.gbxInfo.Text = "Data File Location"
      Me.gbxInfo.Visible = False
      '
      'btnLocate
      '
      Me.btnLocate.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
      Me.btnLocate.Location = New System.Drawing.Point(197, 91)
      Me.btnLocate.Name = "btnLocate"
      Me.btnLocate.Size = New System.Drawing.Size(151, 39)
      Me.btnLocate.TabIndex = 1
      Me.btnLocate.Text = "Locate Data File"
      Me.btnLocate.UseVisualStyleBackColor = False
      '
      'lblMessage
      '
      Me.lblMessage.AutoSize = True
      Me.lblMessage.ForeColor = System.Drawing.Color.Blue
      Me.lblMessage.Location = New System.Drawing.Point(51, 34)
      Me.lblMessage.Name = "lblMessage"
      Me.lblMessage.Size = New System.Drawing.Size(483, 34)
      Me.lblMessage.TabIndex = 0
      Me.lblMessage.Text = "Identify the location where Time, Temperature, and Humidity data" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "should be saved" &
    " to."
      '
      'lblComms
      '
      Me.lblComms.AutoSize = True
      Me.lblComms.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblComms.ForeColor = System.Drawing.Color.Blue
      Me.lblComms.Location = New System.Drawing.Point(498, 328)
      Me.lblComms.Name = "lblComms"
      Me.lblComms.Size = New System.Drawing.Size(535, 26)
      Me.lblComms.TabIndex = 13
      Me.lblComms.Text = "Opening communications.   Testing COMM port : "
      Me.lblComms.Visible = False
      '
      'Label3
      '
      Me.Label3.AutoSize = True
      Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.Label3.ForeColor = System.Drawing.Color.Red
      Me.Label3.Location = New System.Drawing.Point(7, 44)
      Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
      Me.Label3.Name = "Label3"
      Me.Label3.Size = New System.Drawing.Size(180, 31)
      Me.Label3.TabIndex = 14
      Me.Label3.Text = "Temperature"
      '
      'Label5
      '
      Me.Label5.AutoSize = True
      Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.Label5.ForeColor = System.Drawing.Color.Blue
      Me.Label5.Location = New System.Drawing.Point(315, 44)
      Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
      Me.Label5.Name = "Label5"
      Me.Label5.Size = New System.Drawing.Size(128, 31)
      Me.Label5.TabIndex = 15
      Me.Label5.Text = "Humidity"
      '
      'lblTemperature
      '
      Me.lblTemperature.AutoSize = True
      Me.lblTemperature.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblTemperature.ForeColor = System.Drawing.Color.Red
      Me.lblTemperature.Location = New System.Drawing.Point(189, 44)
      Me.lblTemperature.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
      Me.lblTemperature.Name = "lblTemperature"
      Me.lblTemperature.Size = New System.Drawing.Size(100, 31)
      Me.lblTemperature.TabIndex = 16
      Me.lblTemperature.Text = "23.0 C"
      '
      'lblHumidity
      '
      Me.lblHumidity.AutoSize = True
      Me.lblHumidity.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblHumidity.ForeColor = System.Drawing.Color.Blue
      Me.lblHumidity.Location = New System.Drawing.Point(446, 44)
      Me.lblHumidity.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
      Me.lblHumidity.Name = "lblHumidity"
      Me.lblHumidity.Size = New System.Drawing.Size(104, 31)
      Me.lblHumidity.TabIndex = 17
      Me.lblHumidity.Text = "35.0 %"
      '
      'cbxChartLength
      '
      Me.cbxChartLength.FormattingEnabled = True
      Me.cbxChartLength.Items.AddRange(New Object() {"1 Week", "2 Weeks", "1 Month", "2 Month", "3 Month", "6 Month", "9 Month", "1 Year", "Maximum"})
      Me.cbxChartLength.Location = New System.Drawing.Point(771, 63)
      Me.cbxChartLength.Name = "cbxChartLength"
      Me.cbxChartLength.Size = New System.Drawing.Size(85, 24)
      Me.cbxChartLength.TabIndex = 18
      '
      'Label6
      '
      Me.Label6.AutoSize = True
      Me.Label6.ForeColor = System.Drawing.Color.Blue
      Me.Label6.Location = New System.Drawing.Point(659, 65)
      Me.Label6.Name = "Label6"
      Me.Label6.Size = New System.Drawing.Size(102, 17)
      Me.Label6.TabIndex = 19
      Me.Label6.Text = "Chart Length"
      '
      'frmMain
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 16.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
      Me.ClientSize = New System.Drawing.Size(1904, 1001)
      Me.Controls.Add(Me.Label6)
      Me.Controls.Add(Me.cbxChartLength)
      Me.Controls.Add(Me.lblHumidity)
      Me.Controls.Add(Me.lblTemperature)
      Me.Controls.Add(Me.Label5)
      Me.Controls.Add(Me.Label3)
      Me.Controls.Add(Me.lblComms)
      Me.Controls.Add(Me.gbxInfo)
      Me.Controls.Add(Me.txbDirectory)
      Me.Controls.Add(Me.Label4)
      Me.Controls.Add(Me.Label2)
      Me.Controls.Add(Me.txbInterval)
      Me.Controls.Add(Me.Label1)
      Me.Controls.Add(Me.Chart1)
      Me.Controls.Add(Me.MenuStrip1)
      Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.MainMenuStrip = Me.MenuStrip1
      Me.Margin = New System.Windows.Forms.Padding(4)
      Me.Name = "frmMain"
      Me.Text = "Filter Lab Environmental Monitoring                                              " &
    "                                                               by J-KEM Scientif" &
    "ic"
      Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
      Me.MenuStrip1.ResumeLayout(False)
      Me.MenuStrip1.PerformLayout()
      CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
      Me.gbxInfo.ResumeLayout(False)
      Me.gbxInfo.PerformLayout()
      Me.ResumeLayout(False)
      Me.PerformLayout()

   End Sub

   Friend WithEvents MenuStrip1 As MenuStrip
   Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
   Friend WithEvents mnuExit As ToolStripMenuItem
   Friend WithEvents ApplicationToolStripMenuItem As ToolStripMenuItem
   Friend WithEvents mnuDataPath As ToolStripMenuItem
   Friend WithEvents Chart1 As DataVisualization.Charting.Chart
   Friend WithEvents Label1 As Label
   Friend WithEvents txbInterval As TextBox
   Friend WithEvents Label2 As Label
   Friend WithEvents Label4 As Label
   Friend WithEvents txbDirectory As TextBox
   Friend WithEvents mnuRobotDataPath As ToolStripMenuItem
   Friend WithEvents gbxInfo As GroupBox
   Friend WithEvents btnLocate As Button
   Friend WithEvents lblMessage As Label
   Friend WithEvents mnuEnable As ToolStripMenuItem
   Friend WithEvents lblComms As Label
   Friend WithEvents Label3 As Label
   Friend WithEvents Label5 As Label
   Friend WithEvents lblTemperature As Label
   Friend WithEvents lblHumidity As Label
   Friend WithEvents cbxChartLength As ComboBox
   Friend WithEvents Label6 As Label
End Class
