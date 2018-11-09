Imports Symbol.RFID3
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace VB_RFID3Sample6
    Public Class TagStorageForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm)
            Me.m_AppForm = appForm
            Me.InitializeComponent()
            Me.m_Settings = Me.m_AppForm.m_ReaderAPI.Config.GetTagStorageSettings
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing AndAlso (Not Me.components Is Nothing)) Then
                Me.components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Me.mainMenu1 = New MainMenu
            Me.memoryBankSize_TB = New TextBox
            Me.label1 = New Label
            Me.idLength_TB = New TextBox
            Me.maxCount_TB = New TextBox
            Me.hostNameLabel = New Label
            Me.filenameLabel = New Label
            Me.tagStorageSettingButton = New Button
            MyBase.SuspendLayout()
            Me.memoryBankSize_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.memoryBankSize_TB.Location = New Point(&H83, 80)
            Me.memoryBankSize_TB.Name = "memoryBankSize_TB"
            Me.memoryBankSize_TB.Size = New Size(&H68, &H13)
            Me.memoryBankSize_TB.TabIndex = 3
            Me.label1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.label1.Location = New Point(5, 80)
            Me.label1.Name = "label1"
            Me.label1.Size = New Size(&H79, &H1A)
            Me.label1.Text = "Max Size of Memory Bank"
            Me.idLength_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.idLength_TB.Location = New Point(&H83, 50)
            Me.idLength_TB.Name = "idLength_TB"
            Me.idLength_TB.Size = New Size(&H68, &H13)
            Me.idLength_TB.TabIndex = 2
            Me.maxCount_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.maxCount_TB.Location = New Point(&H83, 20)
            Me.maxCount_TB.Name = "maxCount_TB"
            Me.maxCount_TB.Size = New Size(&H68, &H13)
            Me.maxCount_TB.TabIndex = 1
            Me.hostNameLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.hostNameLabel.Location = New Point(5, &H37)
            Me.hostNameLabel.Name = "hostNameLabel"
            Me.hostNameLabel.Size = New Size(&H7A, 14)
            Me.hostNameLabel.Text = "Max Tag ID Length"
            Me.filenameLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.filenameLabel.Location = New Point(5, &H19)
            Me.filenameLabel.Name = "filenameLabel"
            Me.filenameLabel.Size = New Size(&H7C, &H13)
            Me.filenameLabel.Text = "Maximum Tag Count"
            Me.tagStorageSettingButton.Location = New Point(&HB8, &HA1)
            Me.tagStorageSettingButton.Name = "tagStorageSettingButton"
            Me.tagStorageSettingButton.Size = New Size(&H33, 20)
            Me.tagStorageSettingButton.TabIndex = &H17
            Me.tagStorageSettingButton.Text = "Apply"
            AddHandler Me.tagStorageSettingButton.Click, New EventHandler(AddressOf Me.tagStorageSettingButton_Click)
            MyBase.AutoScaleDimensions = New SizeF(96.0!, 96.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Dpi
            Me.AutoScroll = True
            MyBase.ClientSize = New Size(240, &HBC)
            MyBase.Controls.Add(Me.tagStorageSettingButton)
            MyBase.Controls.Add(Me.memoryBankSize_TB)
            MyBase.Controls.Add(Me.label1)
            MyBase.Controls.Add(Me.idLength_TB)
            MyBase.Controls.Add(Me.maxCount_TB)
            MyBase.Controls.Add(Me.hostNameLabel)
            MyBase.Controls.Add(Me.filenameLabel)
            MyBase.Menu = Me.mainMenu1
            MyBase.MinimizeBox = False
            MyBase.Name = "TagStorageForm"
            Me.Text = "Tag Storage"
            AddHandler MyBase.Closing, New CancelEventHandler(AddressOf Me.TagStorageForm_Closing)
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.TagStorageForm_Load)
            MyBase.ResumeLayout(False)
        End Sub

        Friend Sub Reset()
            Me.m_Settings = Me.m_AppForm.m_ReaderAPI.Config.GetTagStorageSettings
        End Sub

        Private Sub TagStorageForm_Closing(ByVal sender As Object, ByVal e As CancelEventArgs)
        End Sub

        Private Sub TagStorageForm_Load(ByVal sender As Object, ByVal e As EventArgs)
            Try
                If Not (Not Me.m_AppForm.m_ReaderAPI.IsConnected OrElse Me.m_IsLoaded) Then
                    Me.maxCount_TB.Text = Me.m_Settings.MaxTagCount.ToString
                    Me.idLength_TB.Text = Me.m_Settings.MaxTagIDLength.ToString
                    Me.memoryBankSize_TB.Text = Me.m_Settings.MaxSizeMemoryBank.ToString
                    Me.m_IsLoaded = True
                End If
            Catch ex As Exception
                Me.m_AppForm.notifyUser(ex.Message, "Tag Storage")
            End Try
        End Sub

        Private Sub tagStorageSettingButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                If Me.m_AppForm.m_ReaderAPI.IsConnected Then
                    Me.m_Settings.MaxTagCount = UInt32.Parse(Me.maxCount_TB.Text)
                    Me.m_Settings.MaxTagIDLength = UInt32.Parse(Me.idLength_TB.Text)
                    Me.m_Settings.MaxSizeMemoryBank = UInt32.Parse(Me.memoryBankSize_TB.Text)
                    Me.m_AppForm.m_ReaderAPI.Config.SetTagStorageSettings(Me.m_Settings)
                End If
                MyBase.Close()
            Catch iue As InvalidUsageException
                Me.m_AppForm.notifyUser(iue.Info, "Tag Storage")
            Catch ofe As OperationFailureException
                Me.m_AppForm.notifyUser(ofe.VendorMessage, "Tag Storage")
            Catch ex As Exception
                Me.m_AppForm.notifyUser(ex.Message, "Tag Storage")
            End Try
        End Sub


        ' Fields
        Private components As IContainer = Nothing
        Private filenameLabel As Label
        Private hostNameLabel As Label
        Private idLength_TB As TextBox
        Private label1 As Label
        Private m_AppForm As AppForm
        Private m_IsLoaded As Boolean
        Private m_Settings As TagStorageSettings
        Private mainMenu1 As MainMenu
        Private maxCount_TB As TextBox
        Private memoryBankSize_TB As TextBox
        Private tagStorageSettingButton As Button
    End Class
End Namespace

