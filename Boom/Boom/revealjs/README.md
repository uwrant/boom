# reveal-zuehlke-template
=======================

A demo presentation for zuehlke based on the [revealjs framework](https://github.com/hakimel/reveal.js/)

#### additional plugins added:
- [Chart](#chart): Easy, object oriented client side graphs for designers and developers [see chartjs.org](http://www.chartjs.org/)


## Instructions

### File Structure

Explain what lies where and show concept of dividing zuehlke-theme and presentation stuff

### Zuehlke Header

How to use; Title on subsections; "before" css magic;

### Chart

Using pie, bar and other fancy charts

### PDF Export

Your presentations can be exported to PDF via a special print stylesheet. This feature requires that you use [Google Chrome](http://google.com/chrome).
Printing on FF might need specific settings for each page and background-images have to be checked if they should appear on the slides.

For printing parallax slides see section [Parallax Background](#paralax-background)

1. Open your presentation with the url parameter ```?print-pdf```
2. Open the in-browser print dialog (CMD+P).
3. Change the **Destination** setting to **Save as PDF**.
5. Change the **Margins** to **None**.
6. Click **Save**.

### Parallax Background

This nice effect with moving background images can be achieved in 2 ways:

###### Option 1
Use the default mode from the reveal project [revealjs framework](https://github.com/hakimel/reveal.js/) (see README -> Parallax Background)

###### Option 2
Use the custom styling from the zuehlke template. This will scale the image to the window size. On the downside you have to enter the px amount which should be moved after each slide.

Edit ```presentation.js``` and change ```var zuehlkeParralaxOffset = 200;```. This will override the default 100px value.  
Edit ```presentation.css```. Below is the demo example... change to fit your needs:  
```html
html.zuehlke-bg-parallax body {
    background-image: url("../images/presentation/sidney_night.jpg");
}

/* Style for print parallax */
body.print-pdf section.zuehlke-bg-parallax{
    /*Make sure paralax background is visible when print mode is active*/
    background: url("../../images/presentation/sidney_night.jpg");
    background-size: auto 100%;
    background-repeat: no-repeat;
}
```