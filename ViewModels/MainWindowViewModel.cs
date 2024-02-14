using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.TreeDataGrid.VariableRowsBug.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace Avalonia.TreeDataGrid.VariableRowsBug.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly Random _random = new ();
        private readonly string[] _words;
        private List<string> _randomWords = new List<string>();

        public MainWindowViewModel()
        {
            try
            {
                _words = ReadAssetFile("1000-most-common-words.txt").Split(new[] { '\r', '\n' });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);                
            }

            for (int i = 0; i < 100; i++)
            {
                _randomWords.Add(GetRandomString());
            }
        }

        private string GetRandomString(int wordCount = 0)
        {
            if (wordCount == 0)
            { 
                wordCount = _random.Next(1, 6); // 1 to 5 words (inclusive)
            }

            var sb = new StringBuilder();
            for (var i = 0; i < wordCount; ++i)
            {
                if (sb.Length > 0)
                    sb.Append(" ");

                sb.Append(_words[_random.Next(_words.Length)]);
            }
            return sb.ToString();
        }

        public static string ReadAssetFile(string assetPath)
        {
            // Determine path
            var assembly = Assembly.GetExecutingAssembly();
            

            using (Stream stream = assembly.GetManifestResourceStream("Avalonia.TreeDataGrid.VariableRowsBug.Assets." + assetPath))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public HierarchicalTreeDataGridSource<string> TreeWrap => new(_randomWords)
        {
            Columns =
            {
                new HierarchicalExpanderColumn<string>
                (
                    new TextColumn<string, string>("Random string", x => x, GridLength.Star, new(){ TextWrapping = TextWrapping.Wrap }),
                    x=> x.Split(' ')
                ),
                new TextColumn<string, string>("Words count", x => (x.Count(c => c == ' ') + 1).ToString(), GridLength.Auto, new (){ TextAlignment = TextAlignment.Center })
            }
        };


        public HierarchicalTreeDataGridSource<string> TreeNoWrap => new(_randomWords)
        {
            Columns =
            {
                new HierarchicalExpanderColumn<string>
                (
                    new TextColumn<string, string>("Random string", x => x, GridLength.Star, new(){ TextWrapping = TextWrapping.NoWrap }),
                    x=> x.Split(' ')
                ), 
                new TextColumn<string, string>("Words count", x => (x.Count(c => c == ' ') + 1).ToString(), GridLength.Auto, new (){ TextAlignment = TextAlignment.Center })
            }
        };

        //protected virtual ITreeDataGridSourceFileNode GetTreeData() => CreateTestTree();

        //private static ITreeDataGridSourceFileNode CreateTestTree()
        //{
        //    var fileEntries = new Dictionary<RelativePath, int>
        //{
        //    { new RelativePath("Some very long file name that takes most of the screen and should show .bsa"), 1 },
        //    { new RelativePath("BWS - Textures.bsa"), 2 },
        //    { new RelativePath("Readme-BWS.txt"), 3 },
        //    { new RelativePath("Textures/greenBlade.dds"), 4 },
        //    { new RelativePath("Textures/greenBlade_n.dds"), 5 },
        //    { new RelativePath("Textures/greenHilt.dds"), 6 },
        //    { new RelativePath("Textures/Armors/greenArmor.dds"), 7 },
        //    { new RelativePath("Textures/Armors/greenBlade.dds"), 8 },
        //    { new RelativePath("Textures/Armors/greenHilt.dds"), 9 },
        //    { new RelativePath("Meshes/greenBlade.nif"), 10 }
        //};

        //    var tree = FileTreeNode<RelativePath, int>.CreateTree(fileEntries);
        //    return TreeDataGridSourceFileNode<RelativePath, int>.FromFileTree(tree);
        //}


    }
}
