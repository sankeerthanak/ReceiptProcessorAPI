Receipt Processor API
Description
The Receipt Processor API is a RESTful API that processes receipts and calculates points based on specific rules. It is built using ASP.NET Core 8.0 and includes a Dockerized setup for easy distribution and deployment.

Features
Submit receipts via the POST /receipts/process endpoint.
Retrieve points for a processed receipt using the GET /receipts/{id}/points endpoint.

Points are calculated based on various rules, such as:
Retailer name length.
Total amount properties.
Item descriptions and purchase date/time.

1. Clone the Repository
git clone https://github.com/sankeerthanak/ReceiptProcessorAPI.git
cd ReceiptProcessorAPI

2. Running with Docker
docker build -t receipt-processor-api
docker run -d -p 5000:80 --name receipt-api receipt-processor-api

Swagger UI:
Open your browser and navigate to:
http://localhost:5000/swagger

Rules for Points Calculation

Retailer Name:
1 point for every alphanumeric character in the retailer name.

Total Amount:
50 points if the total is a round dollar amount (e.g., 10.00).
25 points if the total is a multiple of 0.25.

Items:
5 points for every two items.

Item Descriptions:
If the trimmed length of an item description is a multiple of 3, multiply the item price by 0.2 and round up to the nearest integer. Add this to the points.

Purchase Date:
6 points if the day is odd.

Purchase Time:
10 points if the time is between 2:00 PM and 4:00 PM.
