Feature: Searching from Start page
  As a customer I want to see a list
  of available rooms for booking after I
  enter the dates and no of guests of my desired stay.

  Scenario: Searching on dates from Start page
  Given check in date and check out date is selected 
    And number of people is selected
  When I search 
  Then I should see available rooms for a booking
