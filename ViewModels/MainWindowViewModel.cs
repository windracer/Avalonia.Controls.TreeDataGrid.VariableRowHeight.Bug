using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Media;
using Avalonia.Platform;
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

namespace Avalonia.TreeDataGrid.VariableRowsBug.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly Random _random = new ();
    private readonly string[] _words;
    private List<string> _randomPhrases = new List<string>();

    public HierarchicalTreeDataGridSource<string> TreeWrap => new(_randomPhrases)
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


    public HierarchicalTreeDataGridSource<string> TreeNoWrap => new(_randomPhrases)
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
            _randomPhrases.Add(GetRandomString());
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
}
