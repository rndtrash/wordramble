@echo off
FOR %%A IN (*.svg) DO inkscape %%A --export-width 48 --export-type=png