# Arkray Spotchem SP-4430 PDF Writer
Application can received readings from SP-4430 analyzer and save them as PDF.

## Installation
1. Install application from https://s3.eu-central-1.amazonaws.com/spotchempdf/spotchempdf.application
1. Ignore all security warnings during installation (application is not digitally signed)

## Configuration
Start application to create necessary folder & default configuration files. 

### Configure Serial Port
Serial port has to be configured to correct baudrate - consult with settings of your analyzer.  
Default configuration is
* serial port = COM1
* baudrate = 9600

To configure
1. Navigate in application menu: Nastavenia/Nastav spojenie
1. Select correct COM port from the drop-down box
1. Select correct baudrate from drop-down box

Other parameters are set according to analyzer documentation to:
* 7 data-bits
* even parity
* 2 stop-bits

### Configure output folder for PDF files
1. Navigate in applucation menu: Nastavenia/
