@UsageForecasting
Feature: Get A Usage Forecast For A Customer Account
	In order to facilitate pricing
	As an api consumer
	I want to get a forecast of interval usage for a customer account

	Background:
		Given a usage forecast service
		
	Scenario: Can obtain a forecast of for an account date range
		Given an account
		When getting an {hourly} forecast from {1/1/2015} to {1/31/2015}
		Then a usage forecast response is returned
		Then the response has {744} usages
		Then the minimum usage start date is {1/1/2015}
		Then the maximum usage end date is {1/31/2015}

