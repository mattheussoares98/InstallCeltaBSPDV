using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InstallCeltaBSPDV.Configurations;

namespace InstallCeltaBSPDV.Forms {
    public partial class DownloadFilesForm: Form {
        private readonly EnableConfigurations enable;
        public DownloadFilesForm(EnableConfigurations enable) {
            InitializeComponent();
            this.enable = enable;
            addUrls();
            addItemsInCheckedListBoxUtilities();
            addItemsInCheckedListBoxPrinters();
        }
        private void addItemsInCheckedListBoxUtilities() {
            foreach(string utility in utilities) {
                checkedListBoxUtilities.Items.Add(utility);
            }
            checkedListBoxUtilities.Height = checkedListBoxUtilities.Items.Count * checkedListBoxUtilities.ItemHeight + 5;
        }
        private void addItemsInCheckedListBoxPrinters() {
            foreach(string printer in printers) {
                checkedListBoxPrinters.Items.Add(printer);
            }
            checkedListBoxPrinters.Height = checkedListBoxPrinters.Items.Count * checkedListBoxPrinters.ItemHeight + 5;
        }
        #region Utilities
        private List<string> utilities = new() { ultraVnc, wnb, webConfigSat, teamViewer, layoutKeyboard, manualPDV, driverBooster, winRar };
        private const string ultraVnc = "Ultra VNC";
        private const string wnb = "WNB";
        private const string webConfigSat = "WebConfig SAT";
        private const string teamViewer = "Team Viewer";
        private const string layoutKeyboard = "Layout teclado";
        private const string manualPDV = "Manual do PDV";
        private const string driverBooster = "Driver Booster";
        private const string winRar = "Win rar";
        #endregion
        #region Printers
        private List<string> printers = new() { epsonTMT20, epsonTMT20x, epsonTMT88v, bematechMP4200, swedaSI300S, swedaSI300SIX, darumaDR700, darumaDR800 };
        private const string epsonTMT20 = "Epson TMT20";
        private const string epsonTMT20x = "Epson TMT20x";
        private const string epsonTMT88v = "Epson TMT88v";
        private const string bematechMP4200 = "Bematech MP4200";
        private const string swedaSI300S = "Sweda SI300 S";
        private const string swedaSI300SIX = "Sweda SI300 SIX";
        private const string darumaDR700 = "Daruma DR700";
        private const string darumaDR800 = "Daruma DR800";
        #endregion

        List<string> selectedItemsToDownload = new();
        private Dictionary<string, Dictionary<string, string>> urlsDownloadDictionary = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// logo que inicia a aplicação, já adiciona no Dictionary a chave "nome do arquivo" e outro dictionary dentro dele com o (nome do arquivo + extensão) na chave e url de download no valor
        /// 
        /// Precisei fazer um dictionary dentro do outro porque precisa das seguintes informações: nome do que o usuário selecionou, nome do arquivo com extensão (sem a extensão o arquivo vem sem extensão e não da pra abrir ele a não ser que fique alterando a extensão manualmente) e url de download
        /// 
        /// Quando clica pra baixar os itens, a aplicação verifica todos itens que estão selecionados e insere eles na lista "selectedItemsToDownload"
        /// 
        /// A aplicação percorre o Dictionary através dos valores que estão no "selectedItemsToDownload", vai pegando o  nome do arquivo com a extensão (Keys) pra efetuar os downloads e o valor dele (urls)
        /// </summary>
        private void addUrls() {
            ///MUITO IMPORTANTE
            ///no dictionary que é criado dentro do .Add, precisa ter como chave o nome do arquivo com a extensão

            #region Add utilities
            urlsDownloadDictionary.Add(
                ultraVnc,
                new Dictionary<string, string>() { {
                        $"{ultraVnc}.exe",
                        "https://onedrive.live.com/download?cid=AFE1EC912A7BFA8F&resid=AFE1EC912A7BFA8F%214997&authkey=AP1vKGg27uNIfYc" } });

            urlsDownloadDictionary.Add(
                wnb,
                new Dictionary<string, string>() { {
                        $"{wnb}.exe",
                        "https://onedrive.live.com/download?cid=AFE1EC912A7BFA8F&resid=AFE1EC912A7BFA8F%214995&authkey=AFsRBSObNrphLNM" } });

            urlsDownloadDictionary.Add(
                webConfigSat,
                new Dictionary<string, string>() { {
                        $"web.config", //como precisa ter esse nome, troquei direto aqui
                        "https://onedrive.live.com/download?cid=AFE1EC912A7BFA8F&resid=AFE1EC912A7BFA8F%214994&authkey=ACi166No3DKj3AA" } });

            urlsDownloadDictionary.Add(
               teamViewer,
               new Dictionary<string, string>() { {
                       $"{teamViewer}.exe",
                        "https://onedrive.live.com/download?cid=AFE1EC912A7BFA8F&resid=AFE1EC912A7BFA8F%214996&authkey=AE_4m1tJVrN-Pls" } });

            urlsDownloadDictionary.Add(
                layoutKeyboard,
                new Dictionary<string, string>() { {
                        $"{layoutKeyboard}.zip",
                        "https://onedrive.live.com/download?cid=AFE1EC912A7BFA8F&resid=AFE1EC912A7BFA8F%214993&authkey=AJ0NSoXJ14XJ9cw" } });

            urlsDownloadDictionary.Add(
                manualPDV,
                new Dictionary<string, string>() { {
                        $"{manualPDV}.pdf",
                        "https://onedrive.live.com/download?cid=AFE1EC912A7BFA8F&resid=AFE1EC912A7BFA8F%214992&authkey=AC5HRtxmGuUE2Gc&em=2" } });

            urlsDownloadDictionary.Add(
                driverBooster,
                new Dictionary<string, string>() { {
                        $"{driverBooster}.exe",
                        "https://onedrive.live.com/download?cid=AFE1EC912A7BFA8F&resid=AFE1EC912A7BFA8F%215008&authkey=AN1uR5UlDmJ3fi0" } });

            urlsDownloadDictionary.Add(
                winRar,
                new Dictionary<string, string>() { {
                        $"{winRar}.exe",
                        "https://onedrive.live.com/download?cid=AFE1EC912A7BFA8F&resid=AFE1EC912A7BFA8F%215010&authkey=AEPM34lejQMR1oE" } });
            #endregion

            #region Add printers
            urlsDownloadDictionary.Add(
                epsonTMT20,
                new Dictionary<string, string>() { {
                        $"{epsonTMT20}.zip",
                        "https://onedrive.live.com/download?cid=AFE1EC912A7BFA8F&resid=AFE1EC912A7BFA8F%215011&authkey=AFF-cNlVwH9Q0EQ" } });

            urlsDownloadDictionary.Add(
                    epsonTMT20x,
                    new Dictionary<string, string>() { {
                        $"{epsonTMT20x}.zip",
                        "https://onedrive.live.com/download?cid=AFE1EC912A7BFA8F&resid=AFE1EC912A7BFA8F%215002&authkey=AMvEfzJxYFruux4" } });

            urlsDownloadDictionary.Add(
                    epsonTMT88v,
                    new Dictionary<string, string>() { {
                        $"{epsonTMT88v}.zip",
                        "https://onedrive.live.com/download?cid=AFE1EC912A7BFA8F&resid=AFE1EC912A7BFA8F%215000&authkey=AOj2SlJ7Z2fSXaA" } });

            urlsDownloadDictionary.Add(
                    bematechMP4200,
                    new Dictionary<string, string>() { {
                        $"{bematechMP4200}.zip",
                        "https://onedrive.live.com/download?cid=AFE1EC912A7BFA8F&resid=AFE1EC912A7BFA8F%215003&authkey=AIffZS94PFJdhUc" } });

            urlsDownloadDictionary.Add(
                    swedaSI300S,
                    new Dictionary<string, string>() { {
                        $"{swedaSI300S}.exe",
                        "https://onedrive.live.com/download?cid=AFE1EC912A7BFA8F&resid=AFE1EC912A7BFA8F%215006&authkey=AB1FpFeAGmG7oGQ" } });

            urlsDownloadDictionary.Add(
                    swedaSI300SIX,
                    new Dictionary<string, string>() { {
                        $"{swedaSI300SIX}.exe",
                        "https://onedrive.live.com/download?cid=AFE1EC912A7BFA8F&resid=AFE1EC912A7BFA8F%215007&authkey=AFJx8P9Q_DZy1qA" } });

            urlsDownloadDictionary.Add(
                    darumaDR700,
                    new Dictionary<string, string>() { {
                        $"{darumaDR700}.zip",
                        "https://onedrive.live.com/download?cid=AFE1EC912A7BFA8F&resid=AFE1EC912A7BFA8F%215004&authkey=AKwqxnBMgFzQRhM" } });

            urlsDownloadDictionary.Add(
                    darumaDR800,
                    new Dictionary<string, string>() { {
                        $"{darumaDR800}.zip",
                        "https://onedrive.live.com/download?cid=AFE1EC912A7BFA8F&resid=AFE1EC912A7BFA8F%215005&authkey=ADI7aXN3uidwuw0" } });
            #endregion
        }


        private void addPrinterSelected() {
            //adiciona o nome da impressora que foi selecionado. Esse nome é o mesmo que aparece pro usuário selecionar
            foreach(var printer in checkedListBoxPrinters.CheckedItems) {
                selectedItemsToDownload.Add(printer.ToString());
            }
        }

        void addUtilitiesSelected() {
            //adiciona o nome do "utility" que foi selecionado. Esse nome é o mesmo que aparece pro usuário selecionar
            foreach(var item in checkedListBoxUtilities.CheckedItems) {
                selectedItemsToDownload.Add(item.ToString());
            }
        }

        private async Task downloadSelectedItems() {
            foreach(var item in selectedItemsToDownload) {
                //pega o nome do item selecionado pra consultar o nome do arquivo (com extensão) e url de downlaod pra conseguir iniciar o download
                Dictionary<string, string> urls;
                urlsDownloadDictionary.TryGetValue(item, out urls);

                //precisei fazer mais um foreach pra conseguir pegar a chave (fileName) e valor (url de download) do dictionary que foi selecionado
                foreach(var url in urls) {
                    string fileName = url.Key;
                    string fileUrl = url.Value;
                    await new Download(enable).downloadFileTaskAsync(fileName, fileUrl);
                }
            }
        }

        private async void buttonDownloadFiles_Click(object sender, EventArgs e) {
            selectedItemsToDownload.Clear();

            addUtilitiesSelected();
            addPrinterSelected();

            downloadSelectedItems();
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e) {
            //com isso, só vai permitir selecionar uma impressora pra fazer o download
            for(int ix = 0; ix < checkedListBoxPrinters.Items.Count; ++ix)
                if(ix != e.Index)
                    checkedListBoxPrinters.SetItemChecked(ix, false);
        }
    }
}
