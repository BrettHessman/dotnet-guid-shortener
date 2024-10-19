# dotnet-guid-shortener
Quickly encode and decode GUID into a shorter representation useful for complex URI or aesthetic reasons

## Examples
Base64 encoding:
(for case sensitive scenarios)

```
a93543ce-3964-4900-b295-06026d92040d => OPUNpSWOAkEyWpBC0mkE0A  
12a31d0b-f64d-431a-b3ef-fbd395cc4e89 => L0xoS0k9aMEz+++TXJzOlI  
c7bcc4c6-eedc-4271-8bce-365061626239 => GTMvHzt7xJEL6sNQFmYilD  
```

Base32 encoding:
(for case insensitive scenarios)

```
b3a76f60-ac0d-41f5-aca1-953a75051757 => A33O2ZWBMN5DEMNILJ5UOFYFOF  
654928a4-e72e-4381-a9fb-6a08075c1e0c => EFKSUSZFHPAHEJ56VGE4A4SHYA  
ac764a47-d085-46f3-8cfb-761067b69ace => HSSMHWWQQ64NEM46NHI4MWVG5M  
```


## Performance

```
1000000 Guid -> base32string = (183.923)ms
1000000 base32string -> Guid = (287.5704)ms
1000000 Guid -> base64string = (98.6457)ms
1000000 base64string -> Guid = (209.3176)ms
```

