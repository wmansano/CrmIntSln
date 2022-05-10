namespace CrmDb
{
    using System;

    public partial class crm_email_broadcasts
    {
        public crm_email_broadcasts() {

        }

        public bool Queue() {
            bool success = false;

            try
            {
                success = true;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }
            return success;
        }
    }
}