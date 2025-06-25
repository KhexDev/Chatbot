using System.CodeDom;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using MALO;
using OllamaSharp;
using OllamaSharp.Models.Chat;

namespace Chatbot
{
    public partial class Chatbot : Form
    {
        OllamaApiClient ollama;
        Chat chat;
        string defaultModel;

        public Chatbot()
        {
            Console.WriteLine("Initializing Ollama...");

            string path = "instructions.txt";
            string instructions = "";

            if (File.Exists(path))
            {
                string content = File.ReadAllText(path);
                Console.WriteLine(content);
                instructions = content;
            }
            else
            {
                Console.WriteLine("Couldnt get instructions");
            }

            var uri = new Uri("http://localhost:11434");

            ollama = new OllamaApiClient(uri);
            chat = new Chat(ollama, instructions);
            chat.Messages.Clear();

            var models = ollama.ListLocalModelsAsync();
            models.Wait();

            bool initDefaultModel = false;

            foreach (var model in models.Result.Reverse())
            {
                if (!initDefaultModel)
                {
                    defaultModel = model.Name;
                    initDefaultModel = true;
                }
            }

            ollama.SelectedModel = defaultModel;

            InitializeComponent();

            ModelSelector.Text = defaultModel;

            foreach (var model in models.Result)
            {
                ModelSelector.Items.Add(model.Name);
            }

            InitWebview();
        }

        private async void InitWebview()
        {
            await webView.EnsureCoreWebView2Async(null);
            webView.CoreWebView2.Navigate("https://www.google.com");
            //webView.CoreWebView2.NavigateToString("<h1>Hello, World!<h1/>");
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            string user_input = InputText.Text;
            InputText.Text = "";

            OutputText.Text = "";

            Task.Run(async () =>
            {
                await StreamChatOutput(user_input);
            });
        }

        async private Task StreamChatOutput(string user_input)
        {
            Console.WriteLine("Streaming output");

            await foreach (var answerToken in chat.SendAsAsync(ChatRole.User, user_input, MALO_Tools.tools))
            {
                Console.Write(answerToken);

                OutputText.Invoke((MethodInvoker)delegate
                {
                    this.OutputText.AppendText(answerToken);
                });
            }
        }

        private void ModelSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            var newModel = ModelSelector.Items[ModelSelector.SelectedIndex];
            Console.WriteLine("Changing current model to " + newModel);
            ollama.SelectedModel = (string)newModel;
        }

        private void ClearMessagesButton_Click(object sender, EventArgs e)
        {
            chat.Messages.Clear();
        }
    }
}


namespace MALO {
    public class MALO_Tools
    {
        [OllamaTool]
        public static string CurrentTime() => $"{DateTime.UtcNow.Hour}:{DateTime.UtcNow.Minute}:{DateTime.UtcNow.Second}";

        [OllamaTool]
        public static string ListFiles(string absolutePath)
        {
            string result = "";

            try
            {
                foreach (var file in Directory.GetFiles(absolutePath))
                {
                    Console.WriteLine(file);
                    result += file + "\n";
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return e.ToString();
            }

            return result;
        }

        [OllamaTool]
        public static string WriteToFile(string fileContent) {
            Task.Run(() => File.WriteAllText("generated.txt", fileContent));
            return "Written to file";
        }


        [OllamaTool]
        public static string RunCommand(string command)
        {
            Console.WriteLine("Running Command : " + command);
            Process proc = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c " + command;
            proc.StartInfo = startInfo;
            
            if (proc.Start())
            {
                return "Successfully ran command : " + command;
            }
            else
            {
                return "Failed to run command : " + command;
            }
        }

        [OllamaTool]
        public static string GetFileContent(string path)
        {
            return "File content is:\n" + File.ReadAllText(path);
        }

        [OllamaTool]
        public static string GetSystemOS()
        {
            var name = (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().Cast<ManagementObject>()
                        select x.GetPropertyValue("Caption")).FirstOrDefault();
            Console.WriteLine("OS: " + name);

            return name != null ? name.ToString() : "Unknown";
        }

        public static List<object> tools = [
            new MALO.CurrentTimeTool(), 
            new MALO.ListFilesTool(),
            new MALO.WriteToFileTool(), 
            new MALO.GetFileContentTool(),
            new MALO.RunCommandTool(),
            new MALO.GetSystemOSTool(),
        ];
    }
}

