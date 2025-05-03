# D&D Dungeon Master Assistant Suite

A comprehensive Windows application to help Dungeon Masters run their D&D games more efficiently.

## Features

### Campaign Manager
- Campaign organization with session planning and notes
- NPC database with personality traits, stats, and relationship tracking
- Location management with interactive maps
- Timeline tracking for story progression

### Combat Tracker
- Initiative tracker with auto-sorting
- HP/status effect tracking for monsters and players
- Integrated dice roller with attack/damage calculation
- Condition timer countdown (e.g., spell durations)

### Dynamic Encounter Builder
- Monster database with CR filtering and customization
- Encounter difficulty calculator based on party level/size
- Random encounter generation based on environment
- Treasure generator with appropriate CR rewards

### Real-time Assistant
- Rules reference with quick search functionality
- Name generator for NPCs, locations, taverns
- Ambient sound integration with customizable playlists
- Weather and time of day simulation

### Player-facing Display
- Secondary screen support for player-visible information
- Combat stats visible to players (initiative order, etc.)
- Map sharing with fog of war functionality
- Display art and handouts with annotation capabilities

## Technical Requirements

- Windows 10 or later
- .NET 8 Runtime
- Minimum 4GB RAM
- 1GB free disk space

## Development Setup

1. Install .NET 8 SDK
2. Clone the repository
3. Open the solution in Visual Studio 2022 or later
4. Restore NuGet packages
5. Build and run the application

## Project Structure

- `DMHelper.App`: Main WPF application
  - `Models`: Data models and entities
  - `ViewModels`: MVVM view models
  - `Views`: XAML views and user controls
  - `Services`: Business logic and services
  - `Data`: Database context and repositories

## Contributing

This project is open for contributions. Please follow these guidelines:
1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details. 