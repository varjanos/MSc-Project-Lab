# MSc-Project-Laboratory 1-2

## FloorPlanner ASP.NET + Blazor application

### Specification

My main goal is to create a FloorPlanner application, where the authenticated users can create their own floorplans and manage them.

The application uses .NET's Identity server for authentication, the users must register first, then they can log in.

The app uses WebGL to display the plans on the screen. The user can drag and drop pre-defined items from a menu to the floors.

### Planned functionalities:

- User authentication
- Storing plans, with possible multiple floors
- Adding walls, windows and doors to the floor
- Adding furniture to the floor
- Exporting plans to custom (excel) file
- Optional:
    - Importing own custom file
    - Creating custom furniture
    - Multiuser view

----

### Wireframes

Wireframes can be found on this [Figma page](https://www.figma.com/file/T7iCeyFWDbHn5t4UU6zhn2/FloorPlanner?t=xHwSmCKcp177H2HJ-1).

----

### Weekly schedule

>4. Project specification, DB Scheme, Wireframes, Weekly schedule, Git init

>5. Project (ASP.NET + BLazor), nswag and DB init

>6. Identity server based Authentication, login + registration implementation, DB scheme init

>7. My plans page, new plan popup page implementation                                              

>8. WebGL tutorials, basic edit plan page design

>9. Creating pre-defined Furniture/BuildingObject components to use

>10. Plan drawing, view plan page implementation

>11. Plan drawing implementation

>12. PLan export to custom file

>13. Test, debug, test, debug, test

>14. Documentation

----

### DB Scheme

```cs
class UserProfile
{
    int Id;
    string Name;
    DateTimeOffset RegistrationDate;
    Language Language;
    List<Plan> Plans;
}

class Plan
{
    int Id;
    string Name;
    int BaseWidth;
    int BaseLength;
    DateTimeOffset CreationDate;
    List<Floor> Floors;
}

class Floor
{
    int Id;
    int Width;
    int Length;
    DateTimeOffset LastModificationDate;
    List<BuildingObject> BuildingObjects;
    List<Furniture> Furnitures;
}

class Furniture
{
    int Id;
    FurnitureType Type;
    int X; // Top-left position's X coordinate
    int Y; // Top-left position's Y coordinate
    int Width;
    int Length;
    string ImgPath; // Background img' path
    Material Material;
}

class BuildingObject
{
    int Id;
    BuildingObjectType Type; // Wall, window, door
    int X; // Top-left position's X coordinate
    int Y; // Top-left position's Y coordinate
    int Width;
    int Length;
    int Height;
    Direction? Direction;
}

```