
//------------------------------------------------------------------------------

//  <auto-generated>
//      This code was generated by:
//        TerminalGuiDesigner v1.1.0.0
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// -----------------------------------------------------------------------------
namespace Companion.Console {
    using System;
    using Terminal.Gui;
    
    
    public partial class MainWindow : Terminal.Gui.Window {
        
        private Terminal.Gui.ColorScheme blueOnBlack;
        
        private Terminal.Gui.ColorScheme tgDefault;
        
        private Terminal.Gui.ColorScheme greenOnBlack;
        
        private Terminal.Gui.ColorScheme greyOnBlack;
        
        private Terminal.Gui.Label label6;
        
        private Terminal.Gui.Label label;
        
        private Terminal.Gui.TextField txtFieldPath;
        
        private Terminal.Gui.Label label2;
        
        private Terminal.Gui.RadioGroup radioGroup;
        
        private Terminal.Gui.Label lblStep;
        
        private Terminal.Gui.Label lblTask;
        
        private Terminal.Gui.ProgressBar progressBar;
        
        private Terminal.Gui.Button btnBuyCoffee;
        
        private Terminal.Gui.Button btnGeneratePDF;
        
        private Terminal.Gui.Button btnGithub;
        
        private void InitializeComponent() {
            this.btnGithub = new Terminal.Gui.Button();
            this.btnGeneratePDF = new Terminal.Gui.Button();
            this.btnBuyCoffee = new Terminal.Gui.Button();
            this.progressBar = new Terminal.Gui.ProgressBar();
            this.lblTask = new Terminal.Gui.Label();
            this.lblStep = new Terminal.Gui.Label();
            this.radioGroup = new Terminal.Gui.RadioGroup();
            this.label2 = new Terminal.Gui.Label();
            this.txtFieldPath = new Terminal.Gui.TextField();
            this.label = new Terminal.Gui.Label();
            this.label6 = new Terminal.Gui.Label();
            this.blueOnBlack = new Terminal.Gui.ColorScheme();
            this.blueOnBlack.Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightBlue, Terminal.Gui.Color.Black);
            this.blueOnBlack.HotNormal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Cyan, Terminal.Gui.Color.Black);
            this.blueOnBlack.Focus = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightBlue, Terminal.Gui.Color.BrightYellow);
            this.blueOnBlack.HotFocus = new Terminal.Gui.Attribute(Terminal.Gui.Color.Cyan, Terminal.Gui.Color.BrightYellow);
            this.blueOnBlack.Disabled = new Terminal.Gui.Attribute(Terminal.Gui.Color.Gray, Terminal.Gui.Color.Black);
            this.tgDefault = new Terminal.Gui.ColorScheme();
            this.tgDefault.Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.White, Terminal.Gui.Color.Blue);
            this.tgDefault.HotNormal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightCyan, Terminal.Gui.Color.Blue);
            this.tgDefault.Focus = new Terminal.Gui.Attribute(Terminal.Gui.Color.Black, Terminal.Gui.Color.Gray);
            this.tgDefault.HotFocus = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightBlue, Terminal.Gui.Color.Gray);
            this.tgDefault.Disabled = new Terminal.Gui.Attribute(Terminal.Gui.Color.Brown, Terminal.Gui.Color.Blue);
            this.greenOnBlack = new Terminal.Gui.ColorScheme();
            this.greenOnBlack.Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Green, Terminal.Gui.Color.Black);
            this.greenOnBlack.HotNormal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightGreen, Terminal.Gui.Color.Black);
            this.greenOnBlack.Focus = new Terminal.Gui.Attribute(Terminal.Gui.Color.Green, Terminal.Gui.Color.Magenta);
            this.greenOnBlack.HotFocus = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightGreen, Terminal.Gui.Color.Magenta);
            this.greenOnBlack.Disabled = new Terminal.Gui.Attribute(Terminal.Gui.Color.Gray, Terminal.Gui.Color.Black);
            this.greyOnBlack = new Terminal.Gui.ColorScheme();
            this.greyOnBlack.Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.DarkGray, Terminal.Gui.Color.Black);
            this.greyOnBlack.HotNormal = new Terminal.Gui.Attribute(Terminal.Gui.Color.DarkGray, Terminal.Gui.Color.Black);
            this.greyOnBlack.Focus = new Terminal.Gui.Attribute(Terminal.Gui.Color.Black, Terminal.Gui.Color.DarkGray);
            this.greyOnBlack.HotFocus = new Terminal.Gui.Attribute(Terminal.Gui.Color.Black, Terminal.Gui.Color.DarkGray);
            this.greyOnBlack.Disabled = new Terminal.Gui.Attribute(Terminal.Gui.Color.DarkGray, Terminal.Gui.Color.Black);
            this.Width = Dim.Fill(0);
            this.Height = Dim.Fill(0);
            this.X = 0;
            this.Y = 0;
            this.Visible = true;
            this.ColorScheme = this.greyOnBlack;
            this.Modal = false;
            this.IsMdiContainer = false;
            this.Border.BorderStyle = Terminal.Gui.BorderStyle.Rounded;
            this.Border.BorderBrush = Terminal.Gui.Color.Green;
            this.Border.Effect3D = true;
            this.Border.Effect3DBrush = null;
            this.Border.DrawMarginFrame = true;
            this.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Title = "ProxyFill Companion";
            this.label6.Width = Dim.Percent(100f);
            this.label6.Height = 1;
            this.label6.X = 0;
            this.label6.Y = 1;
            this.label6.Visible = true;
            this.label6.ColorScheme = this.greenOnBlack;
            this.label6.Data = "label6";
            this.label6.Text = "Paste the path to your deck list (.txt) file and click \'Generate PDF\' ";
            this.label6.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.label6);
            this.label.Width = 23;
            this.label.Height = 1;
            this.label.X = 0;
            this.label.Y = 4;
            this.label.Visible = true;
            this.label.ColorScheme = this.greenOnBlack;
            this.label.Data = "label";
            this.label.Text = "Path to ProxyFill order:";
            this.label.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.label);
            this.txtFieldPath.Width = Dim.Percent(100f);
            this.txtFieldPath.Height = 1;
            this.txtFieldPath.X = 0;
            this.txtFieldPath.Y = 5;
            this.txtFieldPath.Visible = true;
            this.txtFieldPath.ColorScheme = this.greyOnBlack;
            this.txtFieldPath.Secret = false;
            this.txtFieldPath.Data = "txtFieldPath";
            this.txtFieldPath.Text = "";
            this.txtFieldPath.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.txtFieldPath);
            this.label2.Width = 3;
            this.label2.Height = 1;
            this.label2.X = 0;
            this.label2.Y = 7;
            this.label2.Visible = true;
            this.label2.ColorScheme = this.greenOnBlack;
            this.label2.Data = "label2";
            this.label2.Text = "Target DPI:";
            this.label2.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.label2);
            this.radioGroup.Width = 10;
            this.radioGroup.Height = 3;
            this.radioGroup.X = 0;
            this.radioGroup.Y = 8;
            this.radioGroup.Visible = true;
            this.radioGroup.ColorScheme = this.greenOnBlack;
            this.radioGroup.Data = "radioGroup";
            this.radioGroup.Text = "";
            this.radioGroup.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.radioGroup.RadioLabels = new NStack.ustring[] {
                    "300dpi",
                    "600dpi",
                    "1200dpi",
                    "Do not compress"
            };
            this.radioGroup.DisplayMode = Terminal.Gui.DisplayModeLayout.Vertical;
            this.Add(this.radioGroup);
            this.lblStep.Width = 30;
            this.lblStep.Height = 1;
            this.lblStep.X = 0;
            this.lblStep.Y = 14;
            this.lblStep.Visible = true;
            this.lblStep.ColorScheme = this.greenOnBlack;
            this.lblStep.Data = "lblStep";
            this.lblStep.Text = "Step: n/a";
            this.lblStep.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.lblStep);
            this.lblTask.Width = 5;
            this.lblTask.Height = 1;
            this.lblTask.X = 0;
            this.lblTask.Y = 15;
            this.lblTask.Visible = true;
            this.lblTask.ColorScheme = this.greenOnBlack;
            this.lblTask.Data = "lblTask";
            this.lblTask.Text = "Task: n/a";
            this.lblTask.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.lblTask);
            this.progressBar.Width = Dim.Fill();
            this.progressBar.Height = 4;
            this.progressBar.X = 0;
            this.progressBar.Y = 16;
            this.progressBar.Visible = true;
            this.progressBar.ColorScheme = this.greenOnBlack;
            this.progressBar.Data = "progressBar";
            this.progressBar.Text = "0%";
            this.progressBar.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.progressBar.Fraction = 1F;
            this.progressBar.BidirectionalMarquee = true;
            this.progressBar.ProgressBarStyle = Terminal.Gui.ProgressBarStyle.MarqueeContinuous;
            this.progressBar.ProgressBarFormat = Terminal.Gui.ProgressBarFormat.FramedPlusPercentage;
            this.progressBar.SegmentCharacter = '█';
            this.Add(this.progressBar);
            this.btnBuyCoffee.Width = 16;
            this.btnBuyCoffee.Height = 1;
            this.btnBuyCoffee.X = 1;
            this.btnBuyCoffee.Y = 21;
            this.btnBuyCoffee.Visible = true;
            this.btnBuyCoffee.ColorScheme = this.greenOnBlack;
            this.btnBuyCoffee.Data = "btnBuyCoffee";
            this.btnBuyCoffee.Text = "BuyMeACoffee";
            this.btnBuyCoffee.TextAlignment = Terminal.Gui.TextAlignment.Centered;
            this.btnBuyCoffee.IsDefault = false;
            this.Add(this.btnBuyCoffee);
            this.btnGeneratePDF.Width = 16;
            this.btnGeneratePDF.Height = 1;
            this.btnGeneratePDF.X = 31;
            this.btnGeneratePDF.Y = 21;
            this.btnGeneratePDF.Visible = true;
            this.btnGeneratePDF.ColorScheme = this.greenOnBlack;
            this.btnGeneratePDF.Data = "btnGeneratePDF";
            this.btnGeneratePDF.Text = "Generate PDF";
            this.btnGeneratePDF.TextAlignment = Terminal.Gui.TextAlignment.Centered;
            this.btnGeneratePDF.IsDefault = false;
            this.Add(this.btnGeneratePDF);
            this.btnGithub.Width = 10;
            this.btnGithub.Height = 1;
            this.btnGithub.X = 67;
            this.btnGithub.Y = 21;
            this.btnGithub.Visible = true;
            this.btnGithub.ColorScheme = this.greenOnBlack;
            this.btnGithub.Data = "btnGithub";
            this.btnGithub.Text = "Github";
            this.btnGithub.TextAlignment = Terminal.Gui.TextAlignment.Centered;
            this.btnGithub.IsDefault = false;
            this.Add(this.btnGithub);
        }
    }
}
