# Taction

A touch panel intended for artists working on Windows Tablet PCs.

![Taction Designer](/docs/taction.png)

## Main Features

* Mouse/Pen/Touch differentiation
  * The panel only responds to touch input. On mouse or pen detection, it automatically hides itself and lets you click on anything under it. As touch-designed buttons tend to be big, this actually doesn't remove any precious screen space.
* Simultaneous Touch+Pen Handling
  * Inferred from the above point. The panel lets you use touch and pen input while working. A typical usage is an eyedropper button; Holding down that button would enable the eyedropper tool and let you pick colour at the same time with the pen until you let go of the button.
* Radial Menu Support
  * For those that fancy an invocable overlay tool selector
* Fairly customizable with little requirement
  * A full layout can be made with a single JSON file. If images or custom font is wanted, they can be grouped into a bundle file for simple distribution.

## Known Minimum Requirement

* Touch device
* .NET Framework 4.6.1 (Comes with Windows 10 November 2015 Update)

## Creating Your Own Layout

### Using the Designer

You may use the designer to generate your own layout. It's currently in early development, but you may [give it a try](https://flamestream.github.io/taction/).

<a href="https://flamestream.github.io/taction/">![Taction Designer](/docs/taction-designer-1.1.png)</a>

### Coding one yourself

At minimum, a JSON file can be used to generate a full layout. It would be validated against the following JSON schema:

> [App/Resources/config.schema.json](App/Resources/config.schema.json)

You may refer to that file for a complete list of supported features and combination. To get started, you may take a look at the default layout the application loads:

> [App/Resources/default.layout.json](App/Resources/default.layout.json)

Once your file is ready, you may load it through the notification icon context menu.

#### Bundle Format

If you want images or font to be included with your layout, you may create a bundle.

Name the JSON layout file as `layout.json` and put all additional resources at the root of a new zip file, then change the extension of the zip file to `.taction-bundle`.
