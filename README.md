# The Fermi Paradox
Personnal project to generate Sci-Fy realistics world/planetary systems
Named after the infamous  https://en.wikipedia.org/wiki/Fermi_paradox
Most of the astronomical data came from http://www.projectrho.com/public_html/rocket/worldbuilding.php


# Goal
The core generate stellar system with stars, planets, asteroids using realistic data (stellar physic, orbital parameters)

Then it defines anomalies climatic and geologic data randomly generated and harmonized.  (life, magnetic fields, civilisations...)

It generate at last "aliens" civilisations.

The secondary output will be color/bump/spec maps of the planets for 3D applications.

# Required Data
A set of csv containing the data table

 - TODO:  Name (using the same system from my python project)

# Architecture
rebuild in progress
14/07/19 : change to csv data source, major rework of the architecture

## The core
The core produces a StellarSystem object from the SystemFactory using data read by the DAL. Object hierarchy is simple:
- ABody is an abstract item with basic information, it allows the creation of "virtual" object (such as barycenter)
- APhysicalBody is a body with mass, angular momentum, but still abstract
- Star,Planet, Blackhole,NeutronStar,GazCloud are the concrete object

And then the orbit object, linking 2 object implementing the interface IOrbitable

The object tree within a stellar system look like this
```
A - orbit - Barycenter - orbit- B
|                   | - orbit - C
|
| - orbit - D - orbit - a
            | - orbit - b
```

The main star A is orbited by a barycenter linking the stars B and C. Another star D orbit A. 2 planets a and b orbit D


## DAL (Data Access Library)
The datasets came from csv files. In order to achieve substitution easily (to a database for instance) the access to the data are managed from a separated dll

## Tests library
There is two test library. One for automated UnitTest and one for manuel console oriented.

# Notes

```shell

copy "$(ProjectDir)bin\Debug\theFermiParadox.Core.dll" "D:\Documents\02-SIDEPROJECT\Orbital\Assets\Plugins"
copy "$(ProjectDir)bin\Debug\theFermiParadox.DAL.dll" "D:\Documents\02-SIDEPROJECT\Orbital\Assets\Plugins"


```
Funny : https://en.wikipedia.org/wiki/The_Fermi_Paradox_Is_Our_Business_Model
