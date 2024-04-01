# Game Design DB 
A database for any designers and journalists of games of any type and genre
with tools for personal archival.

## Motivation
I wanted to find everything written and said by multiple game developers I admire, but there isn't a centralized resource
for finding all of their work and contributions unless they gracefully and regularly update their personalized website with
RSS or Atom support.

## Out of Scope
- Game guides / strategies
- 

## Implementation

### Model
- Game Developers / Designers
- Writings
- Interviews
- Presentations
- Games
- Socials, Blog

### Scrapers
I plan to automate data collection and organization as much as possible through the use of
crawlers and scrapers, specifically on game development blogs, social media, and distribution
platforms.

# TODO
- Add Authors functionality
- Clean code when it comes to ViewModel <> Model conversion and population (not propagation, wrong word)
- Add Games and Resource Collection functionality to PeopleController
- Add Authors and RelatedGames Collection functionality to ResourcesController
- Add multiple websites functionality to PeopleController (expandable inputs with pure HTML/JS ??) or ditch it and go with a single website
- Stylize everything with Bootstrap, darkmode maybe?
- Deploy to Microsoft Azure with GitHub student pack offer
- Write a blog post about the experience
- Add authentication
- Add search functionality to all index pages without losing my mind over boilerplate