﻿namespace Client.Views.Contents
{
    public class ContentCanvas
    {
        public readonly int width;
        public readonly int height;
        private readonly List<ContentString> canvas;

        public ContentCanvas(int width, int height)
        {
            this.width = width;
            this.height = height;
            canvas = new List<ContentString>(FillCanvas());
        }

        private ContentString[] FillCanvas()
        {
            List<ContentString> canvas = new List<ContentString>();
            ContentString line = new ContentString(new string(' ', width));
            for (int i = 0; i < height; i++)
            {
                canvas.Add(line.Clone());
            }
            return canvas.ToArray();
        }

        public ContentString[] GetRows()
        {
            return canvas.ToArray();
        }

        public ContentString? GetRow(int index)
        {
            if (index >= canvas.Count || index < 0) return null;
            return canvas[index];
        }

        public bool SetRow(int rowIndex, ContentString row)
        {
            if (rowIndex >= canvas.Count || rowIndex < 0) return false;
            canvas[rowIndex] = row;
            return true;
        }

        public ContentCharacter? GetColumn(int rowIndex, int columnIndex)
        {
            ContentString? row = GetRow(rowIndex);
            if (row == null) return null;
            if (columnIndex >= row.Length || columnIndex < 0) return null;
            return row[columnIndex];
        }

        public bool SetColumn(int rowIndex, int columnIndex, ContentCharacter column)
        {
            ContentString? row = GetRow(rowIndex);
            if (row == null) return false;
            if (columnIndex >= row.Length || columnIndex < 0) return false;
            row[columnIndex] = column;
            return true;
        }
    }
}
