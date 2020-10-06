#pragma once
template<typename T>
class Node;

template<typename T>
class Stack
{
private:
	Node<T>*	_top;
	int			_size;
public:
	Stack()
	{
		_top	= nullptr;
		_size	= 0;
	};

	~Stack()
	{
		Clear();
	};

	int GetSize() { return _size; };

	void Push(T data) {
		Node<T>* newNode = new Node<T>(data);

		newNode->_next	= _top;
		_top			= newNode;

		_size++;
	};

	T Pop() {
		//스택이 비어있다면 nullptr 반환
		if (nullptr == _top)
		{
			return nullptr;
		}

		Node<T>* retValue = _top;
		_top = _top->_next;
		retValue->_next = nullptr;
		_size--;

		return retValue;
	};

	void Clear()
	{
		_top->Destroy();
	}
};


template<typename T>
class Node
{
public:
	T		_data;
	Node*	_next;

public:
	Node(T data) { _data = data; };
	~Node() {};

	//재귀함수 호출로 뒤에있는 노드도 삭제
	void Destroy()
	{
		if (nullptr != _next) {
			_next->Destroy();

			delete _next;
			_next = nullptr;
		}
	};
};
