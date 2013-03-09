Feature: 
  Main Menu

Background:
	Given I launch the app
	Then I should see a navigation bar titled "CrossBar"

Scenario: I can search for Beers
	When I touch the 2nd view marked "Beers"
	Then I should see a navigation bar titled "Find Beers"

Scenario: I can search for Breweries
	When I touch the 2nd view marked "Breweries"
	Then I should see a navigation bar titled "Find Breweries"

Scenario: I can view my favorite beers
	When I touch the 1st view marked "Beers"
	Then I should see a navigation bar titled "Favorite Beers"

Scenario: I can view my favorite breweries
	When I touch the 1st view marked "Breweries"
	Then I should see a navigation bar titled "Favorite Breweries"
