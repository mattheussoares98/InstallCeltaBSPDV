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
    public partial class DownloadFiles: Form {
        private EnableConfigurations enableConfigurations;
        public DownloadFiles(EnableConfigurations formEnableConfigurations) {
            InitializeComponent();
            enableConfigurations = formEnableConfigurations;
            addUrls();
        }
        private static readonly string ultraVnc = "Ultra VNC";
        private static readonly string wnb = "WNB";
        private static readonly string webConfigSat = "WebConfig SAT";
        private static readonly string teamViewer = "Team Viewer";
        private static readonly string layoutKeyboard = "Layout Teclado";
        private static readonly string manualPDV = "Manual do PDV";

        List<string> selectedItems = new();
        private Dictionary<string, Dictionary<string, string>> urlsDownloadDictionary = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// logo que inicia a aplicação, já adiciona no Dictionary a chave "nome do arquivo" e outro dictionary dentro dele com o (nome do arquivo + extensão) na chave e url de download no valor
        /// 
        /// Precisei fazer um dictionary dentro do outro porque precisa das seguintes informações: nome do que o usuário selecionou, nome do arquivo com extensão e url de download
        /// 
        /// Quando clica pra baixar os itens, a aplicação verifica todos itens que estão selecionados e insere eles na lista "selectedItems"
        /// 
        /// A aplicação percorre o Dictionary através dos valores que estão no "selectedItems", vai pegando o valor dele (urls) e o nome do arquivo com a extensão pra efetuar os downloads
        /// </summary>
        private void addUrls() {
            ///MUITO IMPORTANTE
            ///no dictionary que é criado dentro do .Add, precisa ter como chave o nome do arquivo com a extensão

            #region Utilities
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
                        $"{webConfigSat}.config",
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
            #endregion


            urlsDownloadDictionary.Add(
                "printerTMT20", 
                new Dictionary<string, string>() { { 
                        "Manual do PDV.pdf", 
                        "https://onedrive.live.com/embed?cid=AFE1EC912A7BFA8F&resid=AFE1EC912A7BFA8F%215001&authkey=AN7XOslwTaWq-L" } });
        }


        private void addPrinterSelected() {
            foreach(var printer in checkedListBoxPrinters.CheckedItems) {
                selectedItems.Add(printer.ToString());
            }
        }

        void addUtilitiesSelected() {
            foreach(var item in checkedListBoxUtilities.CheckedItems) {
                selectedItems.Add(item.ToString());
            }
        }

        private void downloadSelectedItems() {
            foreach(var item in selectedItems) {
                Dictionary<string, string> urls;
                urlsDownloadDictionary.TryGetValue(item, out urls);

                //precisei fazer mais um foreach pra conseguir pegar a chave e valor do dictionary que foi selecionado
                foreach(var url in urls) {
                    string fileName = url.Key;
                    string fileUrl = url.Value;
                    Download.downloadFileTaskAsync(fileName, enableConfigurations, fileUrl);

                }
            }
        }

        private async void buttonDownloadFiles_Click(object sender, EventArgs e) {
            selectedItems.Clear();

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
