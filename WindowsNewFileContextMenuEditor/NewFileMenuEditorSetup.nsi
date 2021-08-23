Name "New File Context Menu Editor"
InstallDir "$PROGRAMFILES\Gaxar\New File Context Menu Editor"

Page directory
Page instfiles

Section "New File Context Menu Editor"
	SetOutPath $INSTDIR
	File "newfilemenuedit.exe"
	ExecWait '"$INSTDIR\newfilemenuedit.exe" /register'
	WriteUninstaller "$INSTDIR\uninstall.exe"
	
	WriteRegStr HKLM "Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\NewFileMenuContextEditor" \
                 "DisplayName" "New File Context Menu Editor"
	WriteRegStr HKLM "Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\NewFileMenuContextEditor" \
                 "UninstallString" "$INSTDIR\uninstall.exe"
SectionEnd

UninstPage uninstConfirm
UninstPage instfiles

Section Uninstall
	ExecWait '"$INSTDIR\newfilemenuedit.exe" /unregister'
	Delete "$INSTDIR\newfilemenuedit.exe"
	Delete "$INSTDIR\uninstall.exe"
	RMDir "$INSTDIR"
	
	DeleteRegValue HKLM "Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\NewFileMenuContextEditor" \
					"DisplayName"
	DeleteRegValue HKLM "Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\NewFileMenuContextEditor" \
					 "UninstallString"
	DeleteRegKey HKLM "Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\NewFileMenuContextEditor"
SectionEnd