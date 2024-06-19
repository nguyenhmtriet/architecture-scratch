# 'nswag' tool is needed to generate source out of swagger definition
# Run `nswag new` to generate nswag.json configuration (https://github.com/RicoSuter/NSwag/wiki/NSwag-Configuration-Document)
# How to install:  npm install -g nswag
# Use LoggingApi.json file to configure generator

Invoke-Expression -Command "nswag run LoggingApi.json"

pause