using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAST2D
{
    static class DrawManager
    {

        static List<IDrawable> list;

        static DrawManager()
        {
            list = new List<IDrawable>();
        }


        static public void AddItem(IDrawable item)
        {
            list.Add(item);
        }

        static public void RemoveItem(IDrawable item)
        {
            list.Remove(item);
        }

        static public void Draw()
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Draw();
            }
        }
    }
}
