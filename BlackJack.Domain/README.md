# BlackJack Developer Test

Create an API for the website which plays hands of BlackJack.

- Valid Suits are: Hearts, Diamonds, Spades, Clubs 
- Valid Face values are: Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King

* A Card Consists of a Suit and a Face Value
* A Deck is a collection of cards
* A Hand is a collection of cards
* A Player has a name and a Hand
* A Dealer has a Deck and a Hand
* A Game consists a Dealer and a Player
* At the start of the game the Deck is shuffled and the dealer deals 2 cards to the players hand and 2 to dealers hand
* The Player goes first and has the option to "Hit" or "Stick"
* When the player hits, the dealer deals them another card. If the total of the cards is worth 21 or more, the player is bust and has lost.
* When the player Sticks, the Dealer will hit until their hand is worth at least 17 or greater.
* The Player with the greater score without going bust is the winner.

### Cards are scored as follows
| Face	  | Value   |
|---------| --------|  
| Ace	  | 1 or 11 | 
| Two	  | 2       | 
| Three	  | 3       | 
| Four	  | 4       | 
| Five	  | 5       | 
| Six	  | 6       | 
| Seven	  | 7       | 
| Eight	  | 8       | 
| Nine	  | 9       | 
| Ten	  | 10      | 
| Jack	  | 10      | 
| Queen	  | 10      | 
| King	  | 10      | 
