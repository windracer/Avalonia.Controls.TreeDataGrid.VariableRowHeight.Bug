# BUG in Avalonia.Controls.TreeDataGrid

*AvaloniaUI* 11.0.9 / *TreeDataGrid* 11.0.2

There's visual clutter after scrolling up and down using the keyboard down/up buttons in a TreeDataGrid having variable row height. 

The error is easily reproducible, the same in both Windows and MacOS. 

The demo app contains two TreeDataGrid controls displaying the same data. 

On the left control, the text column has the TextWrapping option set to TextWrapping.NoWrap. The bug isn't reproducible.

On the right control, the text column has the TextWrapping option set to TextWrapping.Wrap (or to TextWrapping.WrapWithOverflow). The bug is reproducible.

Video illustrating the bug in action:

https://github.com/windracer/Avalonia.Controls.TreeDataGrid.VariableRowHeight.Bug/assets/9411121/1e0427db-afe6-4521-877a-b3fe839337c7

