using System;

namespace AttributeTool
{
	public class GameStartAtrribute : Attribute
    {
        public bool isHotfix;
        public GameStartAtrribute(bool isHotfix)
        {
            this.isHotfix = isHotfix;
        }
    }
}

