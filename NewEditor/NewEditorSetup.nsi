Name "NewEditor"
InstallDir "$PROGRAMFILES\Gaxar\NewEditor"

Page directory
Page instfiles

Section "New File Context Menu Editor"
	SetOutPath $INSTDIR
	File "NewEditor.exe"
	ExecWait '"$INSTDIR\NewEditor.exe" /register'
	WriteUninstaller "$INSTDIR\uninstall.exe"
	
	WriteRegStr HKLM "Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\NewEditor" \
                 "DisplayName" "NewEditor"
	WriteRegStr HKLM "Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\NewEditor" \
                 "UninstallString" "$INSTDIR\uninstall.exe"
SectionEnd

UninstPage uninstConfirm
UninstPage instfiles

Section Uninstall
	ExecWait '"$INSTDIR\newfilemenuedit.exe" /unregister'
	Delete "$INSTDIR\NewEditor.exe"
	Delete "$INSTDIR\uninstall.exe"
	RMDir "$INSTDIR"
	
	DeleteRegValue HKLM "Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\NewEditor" \
					"DisplayName"
	DeleteRegValue HKLM "Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\NewEditor" \
					 "UninstallString"
	DeleteRegKey HKLM "Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\NewEditor"
SectionEnd