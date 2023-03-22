npm install nswag -g
nswag run /runtime:Net60

$diffResult = git diff

if([string]::IsNullOrEmpty($diffResult)){
    Write-Host "Client.cs up to date!"
}else {
    Write-Error -Message "Client.cs not up to date!`n$diffResult" -ErrorAction Stop
}