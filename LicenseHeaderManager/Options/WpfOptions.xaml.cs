﻿#region copyright
// Copyright (c) 2011 rubicon IT GmbH

// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
#endregion
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LicenseHeaderManager.Options
{
  public partial class WpfOptions : UserControl
  {
    public OptionsPage Page { get; set; }

    public WpfOptions (OptionsPage page)
    {
      InitializeComponent();

      DataContext = Page = page;

      Loaded += (s, e) =>
      {
        grid.ItemsSource = page.LinkedCommands;
        EnableButtons ();
      };
    }

    private void Add (object sender, RoutedEventArgs e)
    {
      var dialog = new WpfCommandDialog (new LinkedCommand(), Page.Commands);
      bool? result = dialog.ShowDialog ();
      if (result.HasValue && result.Value)
        Page.LinkedCommands.Add (dialog.Command);
    }

    private void Remove (object sender, RoutedEventArgs e)
    {
      var command = grid.SelectedItem as LinkedCommand;
      if (command != null)
        Page.LinkedCommands.Remove (command);
    }

    private void Edit (object sender, RoutedEventArgs e)
    {
      Edit (grid.SelectedItem as LinkedCommand);
    }

    private void OnClick (object sender, MouseButtonEventArgs e)
    {
      if (e.ClickCount == 2)
        Edit (((FrameworkElement) sender).DataContext as LinkedCommand);
    }

    private void Edit (LinkedCommand command)
    {
      if (command == null)
        return;

      var dialog = new WpfCommandDialog (command, Page.Commands);
      dialog.ShowDialog ();
    }

    private void EnableButtons ()
    {
      edit.IsEnabled = remove.IsEnabled = grid.SelectedItem != null;
    }

    private void OnSelectionChanged (object sender, SelectionChangedEventArgs e)
    {
      EnableButtons ();
    }
  }
}
