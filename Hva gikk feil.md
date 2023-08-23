# Ting jeg ville endre

## Måten jeg styla ting på

### Form

Hvordan jeg gjorde det

```css
.wrapper {
  display: flex;
  flex-direction: column;
  margin-top: 20px;
  margin-bottom: 20px;
}

.lable {
  margin-bottom: 5px;
}

.input {
  padding: 14px;
}
```

Hvordan jeg hadde gjort det idag

```css
.form div {
  display: flex;
  flex-direction: column;
  margin-top: 20px;
  margin-bottom: 20px;
}

.form label {
  margin-bottom: 5px;
}

.form input {
  padding: 14px;
}
```

Problemet dette hadde løst er det at jeg har 2000 css filer som ser prikk like ut

Problemet dette potensielt hadde skapt er spesefikk styling på ett enkelt element

```css
.klasse div {
  padding: 14px;
}

.under-klasse {
  padding: 32px;
}
```

siden

```css
.klasse div {
}
```

prioriteres over

```css
.under-klasse {
}
```

Dette kan løses som dette

```css
.klasse div {
  padding: 14px;
}

.under-klasse {
  padding: 32px !important;
}
```

så det er ikke ett kjempe stort problem, men det er ett bestemt valg man må ta, ett valg jeg tror hadde vært passende for denne aplikasjonen siden jeg bare måtte brukt `!important` ett par steder.
For å lese mer om dette se: https://www.w3schools.com/css/css_specificity.asp
