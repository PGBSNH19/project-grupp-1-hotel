Feature: Booking Confirmation e-mail 
  As a Customer I want to receive 
  an email with my booking 
  information after i finalize my booking

  Scenario: Receive email 
    Given that I book a room
    When I finish my Booking
    Then I should be notified by e-mail
