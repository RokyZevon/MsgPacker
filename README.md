# MsgPacker

A simple CLI tool that uses [MessagePack-CSharp](https://github.com/MessagePack-CSharp/MessagePack-CSharp) to convert between MessagePack and JSON, with wildcard support.

## Usage

```shell
MsgPacker <filepath>
```

### Examples

```shell
MsgPacker ./text.msg
```

will convert `./text.msg` to `./output/text.json`.

```shell
MsgPacker ./*.msg
```

will convert all `.msg` files in the current directory to `.json` files in the `./output` directory.
