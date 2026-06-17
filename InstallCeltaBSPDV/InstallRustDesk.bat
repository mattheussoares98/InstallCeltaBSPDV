@echo off
setlocal

:: Define a URL para download, o diretório de instalação e a nova senha
set "url=http://187.35.140.227/downloads/lastversion/Programas/rustdesk.msi"
set "installDir=C:\Install"
set "fileName=rustdesk.msi"
set "msiPath=%installDir%\%fileName%"
:: ============================================================================
:: EDIT THIS LINE WITH YOUR NEW DESIRED PASSWORD
set "newPassword=Celta@123"
:: ============================================================================

:: Verifica se o diretório de instalação existe, e cria se não existir
if not exist "%installDir%" (
    echo Diretorio "%installDir%" nao encontrado. Criando...
    mkdir "%installDir%"
)

:: Baixa o arquivo MSI
echo Baixando o arquivo MSI...
powershell -Command "Invoke-WebRequest -Uri '%url%' -OutFile '%msiPath%'"

:: Verifica se o download foi bem-sucedido
if exist "%msiPath%" (
    echo Download bem-sucedido. Iniciando a instalacao...
    cd /d "%installDir%"
    msiexec /i "%fileName%" CONFIG_HASH=9JSP3JWQ1YXVPFnQLl1aoRGU0clVpNVbulUcwsWQzNTe0RFNwt0R0F0Vy9WYGJiOikXZrJCLiIiOikGchJCLiIiOikXYsVmciwiIyJmLt92YuUmchdXY0xWZj5CdzVnciojI0N3boJye /quiet
    echo Instalacao concluida.

    :: Verifica se o executável do RustDesk está presente no diretório padrão
    if exist "C:\Program Files\RustDesk\rustdesk.exe" (
        echo Executando o RustDesk com a configuracao fornecida...
        "C:\Program Files\RustDesk\rustdesk.exe" --config "host=rust.celtaware.com.br,key=FaorWAtGKp4Tty3sAk0qInmSiVW4PdhkYKBqOUv5Abw="

        :: Define a nova senha para acesso não supervisionado
        echo Definindo a nova senha...
        "C:\Program Files\RustDesk\rustdesk.exe" --password "%newPassword%"
        echo Senha alterada com sucesso.

    ) else (
        echo Erro: O arquivo rustdesk.exe nao foi encontrado no diretorio padrao de instalacao.
    )

) else (
    echo Erro: O download do arquivo falhou. Verifique a URL ou a conexão com a Internet.
)

endlocal