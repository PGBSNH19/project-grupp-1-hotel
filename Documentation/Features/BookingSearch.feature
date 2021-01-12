Feature: Booking search
  As a customer I want to be 
  able to search and find my booking 
  
  Scenario: I want to find my booking
    Given that a booking exists
    When I enter my order number
      And it is a match
    Then I should see my booking with details
