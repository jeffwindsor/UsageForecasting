@Usage
@Profiling
Feature: System Can Shape Summary Usage With An Interval Profile
	As a usage forecaster
    I want to be able to profile summary usage
    So that a the result can be blended with other usage to get an accurate usage forecast for an account
	
	Scenario: The profile completely covers the summary usage
		Given a summary usage for {1000} {kWh} from {1/1/2012} to {1/31/2012}
		  And an hourly profile from {1/1/2012} to {1/31/2012}
		 When the usage forecaster requests the summary usage be profiled 
		 Then the usage engine should return a profiled summary
		  And the profiled summaries should range from {1/1/2012} to {1/31/2012}
		  And the profiled summary values should be equal to the profile value times the unit value

	#In the future we will put in tolerances for gaps, and return zero for value


