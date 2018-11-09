Imports Symbol.RFID3
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace VB_RFID3Sample6
    Public Class LoginForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm)
            Me.m_AppForm = appForm
            Me.InitializeComponent()
        End Sub

        Public Sub clearPassword()
            Me.password_TB.Text = ""
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing AndAlso (Not Me.components Is Nothing)) Then
                Me.components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Me.mainMenu1 = New MainMenu
            Me.readerType_CB = New ComboBox
            Me.password_TB = New TextBox
            Me.username_TB = New TextBox
            Me.IPLabel = New Label
            Me.passwordLabel = New Label
            Me.userNameLabel = New Label
            Me.readerTypeLabel = New Label
            Me.loginButton = New Button
            Me.hostname_TB = New TextBox
            Me.forceLogin_CB = New CheckBox
            MyBase.SuspendLayout()
            Me.readerType_CB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.readerType_CB.Items.Add("XR")
            Me.readerType_CB.Items.Add("FX")
            Me.readerType_CB.Items.Add("MC")
            Me.readerType_CB.Location = New Point(&H65, 12)
            Me.readerType_CB.Name = "readerType_CB"
            Me.readerType_CB.Size = New Size(&H80, 20)
            Me.readerType_CB.TabIndex = 1
            AddHandler Me.readerType_CB.SelectedIndexChanged, New EventHandler(AddressOf Me.readerType_CB_SelectedIndexChanged)
            Me.password_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.password_TB.Location = New Point(&H65, &H53)
            Me.password_TB.Name = "password_TB"
            Me.password_TB.PasswordChar = "*"c
            Me.password_TB.Size = New Size(&H80, &H13)
            Me.password_TB.TabIndex = 3
            Me.username_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.username_TB.Location = New Point(&H65, &H2E)
            Me.username_TB.Name = "username_TB"
            Me.username_TB.Size = New Size(&H80, &H13)
            Me.username_TB.TabIndex = 2
            Me.username_TB.Text = "admin"
            Me.IPLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.IPLabel.Location = New Point(&H3A, &H7C)
            Me.IPLabel.Name = "IPLabel"
            Me.IPLabel.Size = New Size(&H11, 13)
            Me.IPLabel.Text = "IP"
            Me.passwordLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.passwordLabel.Location = New Point(&H16, &H56)
            Me.passwordLabel.Name = "passwordLabel"
            Me.passwordLabel.Size = New Size(&H35, 13)
            Me.passwordLabel.Text = "Password"
            Me.userNameLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.userNameLabel.Location = New Point(15, &H31)
            Me.userNameLabel.Name = "userNameLabel"
            Me.userNameLabel.Size = New Size(60, 13)
            Me.userNameLabel.Text = "User Name"
            Me.readerTypeLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.readerTypeLabel.Location = New Point(9, 15)
            Me.readerTypeLabel.Name = "readerTypeLabel"
            Me.readerTypeLabel.Size = New Size(&H45, 13)
            Me.readerTypeLabel.Text = "Reader Type"
            Me.loginButton.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.loginButton.Location = New Point(&H9A, &H9C)
            Me.loginButton.Name = "loginButton"
            Me.loginButton.Size = New Size(&H4B, &H17)
            Me.loginButton.TabIndex = 5
            Me.loginButton.Text = "Login"
            AddHandler Me.loginButton.Click, New EventHandler(AddressOf Me.loginButton_Click)
            Me.hostname_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.hostname_TB.Location = New Point(&H65, 120)
            Me.hostname_TB.Name = "hostname_TB"
            Me.hostname_TB.Size = New Size(&H80, &H13)
            Me.hostname_TB.TabIndex = 4
            Me.forceLogin_CB.Font = New Font("Tahoma", 8.0!, FontStyle.Regular)
            Me.forceLogin_CB.Location = New Point(&H16, &H9C)
            Me.forceLogin_CB.Name = "forceLogin_CB"
            Me.forceLogin_CB.Size = New Size(100, 20)
            Me.forceLogin_CB.TabIndex = 9
            Me.forceLogin_CB.Text = "Force Login"
            MyBase.AutoScaleDimensions = New SizeF(96.0!, 96.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Dpi
            Me.AutoScroll = True
            MyBase.ClientSize = New Size(240, &HBC)
            MyBase.Controls.Add(Me.forceLogin_CB)
            MyBase.Controls.Add(Me.hostname_TB)
            MyBase.Controls.Add(Me.readerType_CB)
            MyBase.Controls.Add(Me.password_TB)
            MyBase.Controls.Add(Me.username_TB)
            MyBase.Controls.Add(Me.IPLabel)
            MyBase.Controls.Add(Me.passwordLabel)
            MyBase.Controls.Add(Me.userNameLabel)
            MyBase.Controls.Add(Me.readerTypeLabel)
            MyBase.Controls.Add(Me.loginButton)
            MyBase.Menu = Me.mainMenu1
            MyBase.Name = "LoginForm"
            Me.Text = "Login/Logout"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.LoginForm_Load)
            MyBase.ResumeLayout(False)
        End Sub

        Private Sub loginButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                If Me.m_AppForm.m_ReaderAPI.IsConnected Then
                    If (Me.loginButton.Text = "Login") Then
                        Dim info As New LoginInfo
                        info.HostName = Me.hostname_TB.Text
                        info.UserName = Me.username_TB.Text
                        info.Password = Me.password_TB.Text
                        info.SecureMode = SECURE_MODE.HTTP
                        info.ForceLogin = Me.forceLogin_CB.Checked
                        Me.m_AppForm.m_ReaderMgmt.Login(info, DirectCast(Me.readerType_CB.SelectedIndex, READER_TYPE))
                        If Me.m_AppForm.m_ReaderMgmt.IsLoggedIn Then
                            Me.m_AppForm.rebootMenuItem.Enabled = True
                            Me.m_AppForm.antModeMenuItem.Enabled = True
                            Me.m_AppForm.radioPowerMenuItem.Enabled = True
                            Me.m_AppForm.softwareUpdateMenuItem.Enabled = True
                            Me.m_AppForm.systemInfoMenuItem.Enabled = True
                            Me.m_AppForm.m_ReaderType = DirectCast(Me.readerType_CB.SelectedIndex, READER_TYPE)
                            If (Not Nothing Is Me.m_AppForm.m_FirmwareUpdateForm) Then
                                Me.m_AppForm.m_FirmwareUpdateForm.Reset()
                            End If
                        Else
                            Me.m_AppForm.notifyUser("Login Failed", "Reader Login")
                        End If
                    ElseIf (Me.loginButton.Text = "Logout") Then
                        Me.m_AppForm.m_ReaderMgmt.Logout()
                        If Not Me.m_AppForm.m_ReaderMgmt.IsLoggedIn Then
                            Me.m_AppForm.rebootMenuItem.Enabled = False
                            Me.m_AppForm.antModeMenuItem.Enabled = False
                            Me.m_AppForm.radioPowerMenuItem.Enabled = False
                            Me.m_AppForm.softwareUpdateMenuItem.Enabled = False
                            Me.m_AppForm.systemInfoMenuItem.Enabled = False
                        Else
                            Me.m_AppForm.notifyUser("Logout Failed", "Reader Logout")
                        End If
                    End If
                End If
            Catch ex As Exception
                Me.m_AppForm.notifyUser(ex.Message, "Reader Login")
            End Try
            If (Me.loginButton.Text = "Logout") Then
                Me.loginButton.Text = "Login"
            Else
                Me.loginButton.Text = "Logout"
            End If
            Me.hostname_TB.Enabled = Me.username_TB.Enabled = Me.password_TB.Enabled = Me.readerType_CB.Enabled = (Me.loginButton.Text = "Login")
            Me.m_AppForm.configureMenuItemsUponLoginLogout()
            MyBase.Close()
        End Sub

        Private Sub LoginForm_Load(ByVal sender As Object, ByVal e As EventArgs)
            If Not Me.m_IsLoaded Then
                If ((Not Me.m_AppForm.m_ReaderAPI Is Nothing) AndAlso Me.m_AppForm.m_IsConnected) Then
                    Me.hostname_TB.Text = Me.m_AppForm.m_ConnectionForm.IpText
                End If
                Me.readerType_CB.SelectedIndex = 2
                Me.m_IsLoaded = True
            End If
        End Sub

        Private Sub readerType_CB_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim isMCType As Boolean = (Me.readerType_CB.SelectedIndex = 2)
            Me.username_TB.Enabled = Not isMCType
            Me.password_TB.Enabled = Not isMCType
            Me.forceLogin_CB.Enabled = (Me.readerType_CB.SelectedIndex = 1)
        End Sub


        ' Fields
        Private components As IContainer = Nothing
        Private forceLogin_CB As CheckBox
        Private hostname_TB As TextBox
        Private IPLabel As Label
        Friend loginButton As Button
        Private m_AppForm As AppForm
        Private m_IsLoaded As Boolean
        Private mainMenu1 As MainMenu
        Private password_TB As TextBox
        Private passwordLabel As Label
        Private readerType_CB As ComboBox
        Private readerTypeLabel As Label
        Private username_TB As TextBox
        Private userNameLabel As Label
    End Class
End Namespace

