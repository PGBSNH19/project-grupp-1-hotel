Feature: Booking a room
  As a customer I want to be 
  able to pick a room and book it  

  Scenario: Booking a room
    Given a list of available rooms to book
      And a room is selected
      And additional choices have been made
    When I click book
      And email, phone number is entered
    Then my booking should be processed
