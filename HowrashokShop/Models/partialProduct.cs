namespace HowrashokShop.Models
{
    partial class Product
    {
        public string DescToShort
        {
            get
            {
                string desc = "";
                int i = 0; 
                while(i < Description.Length && i < 50)
                {
                    desc += Description[i];
                    i++;
                }
                if(Description.Length > 50)
                {
                    desc += "...";
                }
                return desc;
            }
        }
    }
}
