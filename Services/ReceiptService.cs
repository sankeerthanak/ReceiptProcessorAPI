using ReceiptProcessorAPI.Models; 
using System.Text.RegularExpressions;


namespace ReceiptProcessorAPI.Services
{
    public class ReceiptService
    {
        private static readonly Dictionary<string, Receipt> receipts = new();
        //save receipt into local storage
        public string SaveReceipt(Receipt receipt)
        {
            string id = Guid.NewGuid().ToString();
            receipts[id] = receipt;
            return id;
        }
        //get points for the given retailer id
        public int GetPoints(string id)
        {
            if (!receipts.ContainsKey(id))
                return -1;

            return CalculatePoints(receipts[id]);
        }

        private int CalculatePoints(Receipt receipt)
        {
            int points = 0;

            // One point for every alphanumeric character in the retailer name
            if (!string.IsNullOrEmpty(receipt.Retailer))
            {
                points += Regex.Replace(receipt.Retailer, "[^a-zA-Z0-9]", "").Length;
            }

            // 50 points if the total is a round dollar amount with no cents
            if (!string.IsNullOrEmpty(receipt.Total) && decimal.TryParse(receipt.Total, out decimal total))
            {
                if (total % 1 == 0)
                    points += 50;

                // 25 points if the total is a multiple of 0.25
                if (total % 0.25M == 0)
                    points += 25;
            }

            // 5 points for every two items on the receipt
            if (receipt.Items != null)
            {
                points += (receipt.Items.Count / 2) * 5;

                // Points based on item descriptions
                foreach (var item in receipt.Items)
                {
                    if (!string.IsNullOrEmpty(item.ShortDescription) && !string.IsNullOrEmpty(item.Price) &&
                        decimal.TryParse(item.Price, out decimal itemPrice))
                    {
                        int descLength = item.ShortDescription.Trim().Length;
                        if (descLength % 3 == 0)
                        {
                            points += (int)Math.Ceiling(itemPrice * 0.2M);
                        }
                    }
                }
            }

            // 6 points if the day in the purchase date is odd
            if (DateTime.TryParse(receipt.PurchaseDate, out DateTime purchaseDate) && purchaseDate.Day % 2 == 1)
            {
                points += 6;
            }

            // 10 points if the time of purchase is after 2:00pm and before 4:00pm
            if (TimeSpan.TryParse(receipt.PurchaseTime, out TimeSpan purchaseTime) &&
                purchaseTime.Hours >= 14 && purchaseTime.Hours < 16)
            {
                points += 10;
            }

            return points;
        }


    }
}
