using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;

using NXOpen;
using NXOpen.UF;

public class NXAppViewer : System.Windows.Forms.Form
{
    // NXOpen session
    private static Session theSession;
    private static UFSession theUFSession;    

    // The active NXAppViewer 
    private static NXAppViewer theViewer;

    private Button button_FR;  

    public NXAppViewer()
    {       
        InitializeComponent(); 
    }


#region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.button_FR = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_FR
            // 
            this.button_FR.Location = new System.Drawing.Point(17, 14);
            this.button_FR.Name = "button_FR";
            this.button_FR.Size = new System.Drawing.Size(533, 60);
            this.button_FR.TabIndex = 0;
            this.button_FR.Text = "Click Me";
            this.button_FR.UseVisualStyleBackColor = true;
            this.button_FR.Click += new System.EventHandler(this.button_FR_Click);
            // 
            // NXAppViewer
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
            this.ClientSize = new System.Drawing.Size(559, 84);
            this.Controls.Add(this.button_FR);
            this.Name = "NXAppViewer";
            this.Text = "NXWinFormApp_Csharp";
            this.ResumeLayout(false);

    }
#endregion    

	public static void Main() 
    {
        theSession = Session.GetSession();
        theUFSession = UFSession.GetUFSession();
        

        theViewer = new NXAppViewer();
        theViewer.Show();
    }  

    private void button_FR_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Debugger.Launch();
        try
        {            
            Part workPart = theSession.Parts.Work;
            BodyCollection workBody = theSession.Parts.Work.Bodies;
            Body[] arrBody = workBody.ToArray();
            for (int i = 0; i < arrBody.Length; i++)
            {
                NXOpen.Body thisBody = arrBody[i];
                NXOpen.Face[] faces = thisBody.GetFaces();

                for (int j = 0; j < faces.Length; j++)
                {
                    NXOpen.Face thisFace = faces[j];
                    if (thisFace.SolidFaceType == Face.FaceType.Cylindrical)
                    {                        
                        Console.WriteLine("Cylinder face " + j.ToString());                        
                    }
                    else if (thisFace.SolidFaceType == Face.FaceType.Planar)
                    {
                        Console.WriteLine("Plane face " + j.ToString());                         
                    }
                }
            }

        }
        catch (NXException ex)
        {
            UI.GetUI().NXMessageBox.Show("Message", NXMessageBox.DialogType.Error, ex.Message);
        }          
    }   

} 
