# this is for use in Team City
#nuget.exe config
Nuget.exe install .eT\eT\code\Shell\packages.config -OutputDirectory .eT\eT\code\packages
Nuget.exe install .eT\eT\code\Trading\packages.config -OutputDirectory .eT\eT\code\packages
Nuget.exe install .eT\eT\code\Modules\packages.config -OutputDirectory .eT\eT\code\packages
Nuget.exe install .eT\eT\code\DataGridWithColumnStyle\packages.config -OutputDirectory .eT\eT\code\packages
Nuget.exe install .eT\eT\code\p1\packages.config -OutputDirectory .eT\eT\code\packages
#Start-Sleep -s 600