# Overview

This project was all about simulating the behavior of light, specifically diffraction using the Fresnel-Huygens principle, which essentially states that when a wavefront hits an aperture, every single point inside the aperture can be treated as a wavefront.

To use this principle, I wrote many classes. The most important are the Aperture and Backstop classes. These simulate of course, the aperture where the light shines through, and the backstop, which is just my term for a flat screen where the diffracted light shines onto. I also have a Color class which helps me translate wavelenths of light into colors. In the future, I'd like to be able to handle white light, and other hues which can't be described by discreet wavelengths.

This is a long-time goal of mine, and I finally got the opportnuity to sink some time into it. I really enjoyed this process, and I will likely improve upon this software in the future.

[Software Demo Video](https://www.youtube.com/watch?v=mXuoq37A-_w)

# Development Environment

To make this project possible, I used C# with VSCode, and I used a simple P3 PPM file format for my image outputs. I used System.IO and System.Numerics to handle file output and complex numbers.

# Useful Websites

- [Wikipedia - Huygens–Fresnel principle](https://en.wikipedia.org/wiki/Huygens%E2%80%93Fresnel_principle)
- [Wikipedia - Diffraction](https://en.wikipedia.org/wiki/Diffraction)

# Future Work

- Be able to iterate over many wavelengths
- Be able to change backstop sive and location from the aperture
- Include less accurate but faster algorithms, like FFTs
