using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace InstallCeltaBSPDV.Configurations {
    public static class Windows {
        public static async Task enableAllPermissionsForPath(string path, RichTextBox richTextBoxResults) {

            #region Edit permissions

            const FileSystemRights rights = FileSystemRights.FullControl;

            var allUsers = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);

            // Add Access Rule to the actual directory itself
            var accessRule = new FileSystemAccessRule(
                allUsers,
                rights,
                InheritanceFlags.None,
                PropagationFlags.NoPropagateInherit,
                AccessControlType.Allow);

            if(!Directory.Exists(path)) {
                richTextBoxResults.Text += $"O caminho {path} não foi encontrado para habilitar a permissão para todos usuários\n\n";
                return;
            }

            var info = new DirectoryInfo(path);
            var security = info.GetAccessControl(AccessControlSections.Access);

            bool result;
            security.ModifyAccessRule(AccessControlModification.Set, accessRule, out result);

            if(!result) {
                throw new InvalidOperationException("Failed to give full-control permission to all users for path " + path);
            } else {
                richTextBoxResults.Text += $"Adicionado permissão para todos usuários na pasta {path}\n\n";
            }

            // add inheritance
            var inheritedAccessRule = new FileSystemAccessRule(
                allUsers,
                rights,
                InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                PropagationFlags.InheritOnly,
                AccessControlType.Allow);

            bool inheritedResult;
            security.ModifyAccessRule(AccessControlModification.Add, inheritedAccessRule, out inheritedResult);

            if(!inheritedResult) {
                throw new InvalidOperationException("Failed to give full-control permission inheritance to all users for " + path);
            }
            await Task.Run(() => info.SetAccessControl(security));


            #endregion
        }

        public static async Task movePath(string sourcePath, string destinyPath, RichTextBox richTextBoxResults) {
            if(!Directory.Exists(sourcePath)) {
                MessageBox.Show($"Não foi possível encontrar o caminho {sourcePath}");
                return;
            }

            if(Directory.Exists(destinyPath)) {
                MessageBox.Show($"Como o diretório {destinyPath} já existe, não fará a cópia da pasta para o diretório");
                return;
            }


            try {
                //Directory.Move(sourcePath, destinyPath);
                Task.Delay(7000).Wait();
                await Task.Run(() => Directory.Move(sourcePath, destinyPath));
                richTextBoxResults.Text += $"{sourcePath} movido com sucesso para o caminho {destinyPath}\n\n";
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        public static async Task extractFile(string sourceFilePath, string destinyPath, string fileName, CheckBox checkBoxToMark = null) {
            if(!File.Exists(sourceFilePath)) {
                DialogResult dialogResult = MessageBox.Show($"O {sourceFilePath} não existe. Deseja baixá-lo?", "Aviso!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                if(dialogResult == DialogResult.Yes) {
                    try {
                        await Utils.downloadFileTaskAsync(fileName);
                    } catch(Exception ex) {
                        MessageBox.Show($"Erro para baixar o {fileName}: {ex.Message}");
                    }
                } else {
                    return;
                }
            }

            if(File.Exists(sourceFilePath)) {
                try {
                    await Task.Run(() => ZipFile.ExtractToDirectory(sourceFilePath, destinyPath, true));
                    //mesmo colocando o await acima, parece que estava indo pro próximo passo sem terminar a execução da extração dos arquivos
                    checkBoxToMark.Checked = true;
                    //checkBoxToMark.ForeColor = Color.Green;
                } catch(Exception ex) {
                    MessageBox.Show($"Erro para extrair o {fileName}: {ex.Message}");
                }
            }
        }
    }
}
