     1 2 3 4 5 6 7
```
0 -> a,b,c,e,f,g
1 -> c,f
2 -> a,c,d,e,g
3 -> a,c,d,f,g
4 -> b,c,d,f
5 -> a,b,d,f,g
6 -> a,b,d,e,f,g
7 -> a,c,f
8 -> a,b,c,d,e,f,g
9 -> a,b,c,d,f,g
```

---

```
a -> a,b,c,d,e,f,g
b -> a,b,c,d,e,f,g
c -> a,b,c,d,e,f,g
d -> a,b,c,d,e,f,g
e -> a,b,c,d,e,f,g
f -> a,b,c,d,e,f,g
g -> a,b,c,d,e,f,g
```
```
acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf
```
---

1: acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb **ab** | cdfeb fcadb cdfeb cdbaf --> 1

```
a -> c,f
b -> c,f
c -> a,b,c,d,e,f,g
d -> a,b,c,d,e,f,g
e -> a,b,c,d,e,f,g
f -> a,b,c,d,e,f,g
g -> a,b,c,d,e,f,g
```

---

2: acedgfb cdfbe gcdfa fbcad **dab** cefabd cdfgeb eafb cagedb *ab* | cdfeb fcadb cdfeb cdbaf --> 7

```
a -> c,f
b -> c,f
c -> a,b,c,d,e,f,g
d -> a,c,f
e -> a,b,c,d,e,f,g
f -> a,b,c,d,e,f,g
g -> a,b,c,d,e,f,g
```

---

3: acedgfb cdfbe gcdfa fbcad *dab* cefabd cdfgeb **eafb** cagedb *ab* | cdfeb fcadb cdfeb cdbaf --> 4

```
a -> c,f
b -> c,f
c -> a,b,c,d,e,f,g
d -> a,c,f
e -> b,c,d,f
f -> b,c,d,f
g -> a,b,c,d,e,f,g
```

---

4: Zweimal gleicher Output mit zwei optionen --> Diese zwei müssen dort vorkommen und können bei den anderen gestrichen werden.

4.1:
```
a -> |c,f|
b -> |c,f|
c -> a,b,d,e,g
d -> a
e -> b,d
f -> b,d
g -> a,b,d,e,g
```

4.2:
```
a -> |c,f|
b -> |c,f|
c -> a,e,g
d -> a
e -> |b,d|
f -> |b,d|
g -> a,e,g
```

---

5: Einzelne outputs sind fest und können bei den anderen gestrichen werden.

```
a -> |c,f|
b -> |c,f|
c -> e,g
d -> |a|
e -> |b,d|
f -> |b,d|
g -> e,g
```

---

4 & 5 abwechselnd wiederholen, bis alle "fest" sind.

4:
```
a -> |c,f|
b -> |c,f|
c -> |e,g|
d -> |a|
e -> |b,d|
f -> |b,d|
g -> |e,g|
```

---

7: Inputs und Outputs durchlaufen und schauen ob es nur eine mögliche kombination für die paare gibt, damit ein valides Muster rauskommt.

*acedgfb* **cdfbe** gcdfa fbcad *dab* cefabd cdfgeb *eafb* cagedb *ab* | cdfeb fcadb cdfeb cdbaf

`[eg][a][bd][cf][bd]`

5 -> a,b,d,f,g
2 -> a,c,d,e,g
3 -> a,c,d,f,g

1. `eabcd` `000` --> Nichts
2. `gabcd` `100` --> Nichts
3. `eadcb` `010` --> Nichts
4. `gadcb` `110` --> Nichts
5. `eabfd` `001` --> Nichts
6. `gabfd` `101` --> 5
7. `eadfb` `011` --> Nichts
8. `gadfb` `111` --> Nichts

--> 5
```
a -> |c|
b -> |f|
c -> |g|
d -> |a|
e -> |d|
f -> |b|
g -> |e|
```

**Fertig!!!**















































































































































































