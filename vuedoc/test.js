console.log("testing....");

const vuedoc = require('@vuedoc/parser')
const Parser = require('@vuedoc/parser/lib/parser')

// stevelamb:~/Development/ibcos/investigations/vuedoc : (master) $ ls ../../../ibcos/priceupdates/app/src/modules/priceupdates/views/ManufacturerMappings.vue 

/*
const options = {
  filename: '../../../ibcos/priceupdates/app/src/modules/priceupdates/views/ManufacturerMappings.vue',
  features: Parser.SUPPORTED_FEATURES.filter((feature) => feature !== 'data')
}
*/
/*
vuedoc.parse(options)
  .then((component) => Object.keys(component))
  .then((keys) => console.log(keys)) // => { name, description, keywords, props, computed, events, methods }
  .catch((err) => console.error(err))
*/

var myComponent;

function display(component){
    console.log(component);
    myComponent = component;
}

const options = {
    filename: '../../../ibcos/priceupdates/app/src/modules/priceupdates/views/ManufacturerMappings.vue'
  }
  



vuedoc.parse(options)
    .then((component) => display(component));

    
console.log(myComponent);
console.log('done');

