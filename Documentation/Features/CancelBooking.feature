Feature: Cancel Booking
  As a customer I want to be 
  able to cancel my booking on my own

  Scenario: Cancel my booking
    Given that I have searched on my Booking
    When I enter my e-mail
      And it matches the e-mail entered with the Booking
    Then I should have canceled my booking
