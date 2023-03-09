﻿// This is a personal academic project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Geoinformatika
{
    public partial class LayerControl : UserControl
    {
        public LayerControl()
        {
            InitializeComponent();
        }
        public Map map;
        public void RefreshList()
        {
            listView1.BeginUpdate();
            listView1.Clear();
            if (map == null)
            {
                return;
            }
            int i = 0;
            foreach (AbstractLayer layer in map.Layers)
            {
                i++;
                if (layer.Name != "Noname")
                {
                    ListViewItem listViewItem = listView1.Items.Insert(0, layer.Name);
                    listViewItem.Checked = layer.Visible;
                    listViewItem.Tag = layer;
                }

            }
            listView1.EndUpdate();
        }

        private void listView1_ItemCheck(object sender, ItemCheckEventArgs e)
        {

        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            AbstractLayer layer = (AbstractLayer)e.Item.Tag;
            if (layer == null)
            {
                return;
            }
            layer.Visible = e.Item.Checked;
            if (map == null)
            {
                return;
            }
            map.Refresh();

            //Layer layer2 = (e.Item.Tag) as Layer;
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (sender != listView1)
            {
                return;
            }

            e.Effect = e.AllowedEffect;
        }

        private void listView1_DragLeave(object sender, EventArgs e)
        {
            if (sender != listView1)
            {
                return;
            }

            listView1.InsertionMark.Index = -1;
        }

        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (sender != listView1)
            {
                return;
            }

            listView1.DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void listView1_DragOver(object sender, DragEventArgs e)
        {
            if (sender != listView1)
            {
                return;
            }

            System.Drawing.Point targetPoint = listView1.PointToClient(new System.Drawing.Point(e.X, e.Y));

            int targetIndex = listView1.InsertionMark.NearestIndex(targetPoint);
            if (targetIndex > -1)
            {
                Rectangle itemBounds = listView1.GetItemRect(targetIndex);
                if (targetPoint.X > itemBounds.Left + (itemBounds.Width / 2))
                {
                    listView1.InsertionMark.AppearsAfterItem = true;
                }
                else
                {
                    listView1.InsertionMark.AppearsAfterItem = false;
                }
            }

            listView1.InsertionMark.Index = targetIndex;
        }

        public void SinListWithMap()
        {
            if (map == null)
            {
                return;
            }

            List<AbstractLayer> tempLayers = new List<AbstractLayer>();

            for (int i = listView1.Items.Count - 1; i >= 0; i--)
            {
                tempLayers.Add(listView1.Items[i].Tag as AbstractLayer);
            }

            map.Layers = tempLayers;
            map.Refresh();
        }
        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            if (sender != listView1)
            {
                return;
            }

            int targetIndex = listView1.InsertionMark.Index;
            if (targetIndex == -1)
            {
                return;
            }

            if (listView1.InsertionMark.AppearsAfterItem)
            {
                targetIndex++;
            }
            ListViewItem draggetItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));

            Map oldMap = map;
            map = null;

            listView1.Items.Insert(targetIndex+1, (ListViewItem)draggetItem.Clone());
            map = oldMap;

            listView1.Items.Remove(draggetItem);
            SinListWithMap();
            RefreshList();

        }
    }

}

