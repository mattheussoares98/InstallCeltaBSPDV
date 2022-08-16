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
using InstallCeltaBSPDV.Forms.DownloadFiles;

namespace InstallCeltaBSPDV.Forms {
    public partial class DownloadFilesForm: Form {
        private readonly EnableConfigurations enable;
        public DownloadFilesForm(EnableConfigurations enable) {
            InitializeComponent();
            this.enable = enable;
            new Printers(this);
            new SATs(this);
            new Utilities(this);
            new PinPads(this);
            new Keyboards(this);
        }
        private void buttonDownloadFiles_Click(object sender, EventArgs e) {
            selectedItemsToDownload.Clear();

            addUtilitiesSelected();
            addPrinterSelected();
            addSatSelected();
            addPinPadSelected();
            addKeyboardsSelected();

            downloadSelectedItems();
        }

        List<string> selectedItemsToDownload = new();
        internal Dictionary<string, Dictionary<string, string>> urlsDownloadDictionary = new Dictionary<string, Dictionary<string, string>>();

        private void addPrinterSelected() {
            //adiciona o nome da impressora que foi selecionado. Esse nome é o mesmo que aparece pro usuário selecionar
            foreach(var printer in checkedListBoxPrinters.CheckedItems) {
                selectedItemsToDownload.Add(printer.ToString());
            }
        }
        private void addSatSelected() {
            //adiciona o nome da impressora que foi selecionado. Esse nome é o mesmo que aparece pro usuário selecionar
            foreach(var sat in checkedListBoxSats.CheckedItems) {
                selectedItemsToDownload.Add(sat.ToString());
            }
        }
        private void addPinPadSelected() {
            //adiciona o nome da impressora que foi selecionado. Esse nome é o mesmo que aparece pro usuário selecionar
            foreach(var pinpad in checkedListBoxPinPads.CheckedItems) {
                selectedItemsToDownload.Add(pinpad.ToString());
            }
        }

        private void addUtilitiesSelected() {
            //adiciona o nome do "utility" que foi selecionado. Esse nome é o mesmo que aparece pro usuário selecionar
            foreach(var item in checkedListBoxUtilities.CheckedItems) {
                selectedItemsToDownload.Add(item.ToString());
            }
        }
        private void addKeyboardsSelected() {
            //adiciona o nome do "Keyboard" que foi selecionado. Esse nome é o mesmo que aparece pro usuário selecionar
            foreach(var item in checkedListBoxKeyboards.CheckedItems) {
                selectedItemsToDownload.Add(item.ToString());
            }
        }

        private void downloadSelectedItems() {
            foreach(var item in selectedItemsToDownload) {
                //pega o nome do item selecionado pra consultar o nome do arquivo (com extensão) e url de downlaod pra conseguir iniciar o download
                Dictionary<string, string> namesAndUrls;
                urlsDownloadDictionary.TryGetValue(item, out namesAndUrls);

                //precisei fazer mais um foreach pra conseguir pegar a chave (fileName) e valor (url de download) do dictionary que foi selecionado
                foreach(var nameAndUrl in namesAndUrls) {
                    string fileName = nameAndUrl.Key;
                    string fileUrl = nameAndUrl.Value;
                    new Download(enable).downloadFileTaskAsync(fileName, fileUrl);
                }
            }
        }

        /// <summary>
        /// só permite marcar um item nas impressoras
        /// </summary>
        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e) {
            //com isso, só vai permitir selecionar uma impressora pra fazer o download
            for(int ix = 0; ix < checkedListBoxPrinters.Items.Count; ++ix)
                if(ix != e.Index)
                    checkedListBoxPrinters.SetItemChecked(ix, false);
        }

        /// <summary>
        /// só permite marcar um item nos SATs
        /// </summary>
        private void checkedListBoxSats_ItemCheck(object sender, ItemCheckEventArgs e) {
            for(int ix = 0; ix < checkedListBoxSats.Items.Count; ++ix)
                if(ix != e.Index)
                    checkedListBoxSats.SetItemChecked(ix, false);
        }

        /// <summary>
        /// só permite marcar um item nos PinPads
        /// </summary>
        private void checkedListBoxPinPads_ItemCheck(object sender, ItemCheckEventArgs e) {
            for(int ix = 0; ix < checkedListBoxPinPads.Items.Count; ++ix)
                if(ix != e.Index)
                    checkedListBoxPinPads.SetItemChecked(ix, false);
        }


        private void checkedListBoxKeyboards_ItemCheck(object sender, ItemCheckEventArgs e) {
            for(int ix = 0; ix < checkedListBoxKeyboards.Items.Count; ++ix)
                if(ix != e.Index)
                    checkedListBoxKeyboards.SetItemChecked(ix, false);
        }
    }
}
