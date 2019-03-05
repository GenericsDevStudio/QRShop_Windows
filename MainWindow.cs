using System;
using Gtk;
using test1;

public partial class MainWindow : Gtk.Window
{
    ListStore myList = new ListStore(typeof(string));
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
        TreeViewColumn art = new TreeViewColumn();
        art.Title = "art";
        treeview3.AppendColumn(art);
        CellRendererText artC = new CellRendererText();
        art.PackStart(artC, true);
        art.AddAttribute(artC, "text", 0);
        treeview3.Model = myList;

        treeview3.Selection.Changed += (sender, e) => {
            Console.WriteLine("SELECTION WAS CHANGED");
            Gtk.TreeIter selected;
            if (treeview3.Selection.GetSelected(out selected))
            {
                label1.Text = (string)myList.GetValue(selected, 0);
                //Console.WriteLine("SELECTED ITEM: {0}", model.GetValue(selected, 0)));
            }
        };
    }

    private void foo(object sender, EventArgs arg)
    {
        TreeIter iter;

        TreePath[] treePath = treeview3.Selection.GetSelectedRows();

        for (int i = treePath.Length; i > 0; i--)
        {
            myList.GetIter(out iter, treePath[(i - 1)]);
            string value = (string)myList.GetValue(iter, 0);
            label1.Text = value;
            //Console.WriteLine("Removing: " + value);

            myList.Remove(ref iter);
        }
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    private void buttonClickMe(object sender, EventArgs args)
    {

        myList.AppendValues(entry2.Text);
    }

    protected void deleteclick(object sender, EventArgs e)
    {
        TreeIter iter;

        TreePath[] treePath = treeview3.Selection.GetSelectedRows();

        for (int i = treePath.Length; i > 0; i--)
        {
            myList.GetIter(out iter, treePath[(i - 1)]);

            //string value = (string)myList.GetValue(iter, 0);
            //Console.WriteLine("Removing: " + value);

            myList.Remove(ref iter);
        }
    }
}
