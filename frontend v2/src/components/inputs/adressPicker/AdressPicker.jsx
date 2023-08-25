import React, { useState } from "react";
import {
  GeoapifyGeocoderAutocomplete,
  GeoapifyContext,
} from "@geoapify/react-geocoder-autocomplete";
import "@geoapify/geocoder-autocomplete/styles/minimal.css";

const AdressPicker = () => {
  const [val, setVal] = useState();

  function onPlaceSelect(value) {
    setVal(value);
    console.log("Selected Place:", value);
  }

  function onSuggectionChange(value) {
    console.log("Suggestions Changed:", value);
  }

  return (
    <GeoapifyContext apiKey="55435eaae6f045e2a155927ccf342b23">
      <GeoapifyGeocoderAutocomplete
        onPlaceSelect={onPlaceSelect}
        onSuggestionsChange={onSuggectionChange}
        lang="no"
      />
    </GeoapifyContext>
  );
};

export default AdressPicker;
