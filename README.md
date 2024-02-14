# BUG in Avalonia.Controls.TreeDataGrid

There's visual clutter after scrolling up and down using the keyboard down/up buttons in a TreeDataGrid having variable row height. 

The error is easily reproducible, the same in both Windows and MacOS. 

The demo app contains two TreeDataGrid controls displaying the same data. 

On the left control, the text column has the TextWrapping option set to TextWrapping.NoWrap. The bug isn't reproducible.

On the right control, the text column has the TextWrapping option set to TextWrapping.Wrap (or to TextWrapping.WrapWithOverflow). The bug is reproducible.



