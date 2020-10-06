#pragma once

template<typename T>
class Node;

template<typename T>
class LinkedList
{
public :

private:
	Node<T>*	_head;
	int			_size;

public:
	LinkedList()
	{
		_head		= nullptr;
		_size		= 0;
	};

	~LinkedList()
	{
		Clear();
	};

	int	GetSize() { return _size; };

	void Insert(T data) {
		if (nullptr == _head)
		{
			_head = new Node<T>(data);
			_size++;
		}
		else
		{
			Node<T>* current = _head;
			while (nullptr != current->_next)
			{
				current = current->_next;
			}

			current->_next = new Node<T>(data);
			_size++;
		}
	};

	bool InsertAt(T data, int pos) {
		//맨앞에 추가할 경우
		if (0 == pos)
		{
			Node<T>* newNode	= new Node<T>(data);
			newNode->_next		= _head;
			_head				= newNode;

			return true;
		}

		//맨앞이 아닌 곳에 추가할 경우
		Node<T>* node = this.GetNode(pos - 1);

		if (nullptr == node) return false;		//노드를 가져오는데 실패하면 false를 리턴하고 종료

		Node<T>* newNode	= new Node<T>(data);
		newNode->_next		= node->_next();
		node->_next			= newNode;

		return true;
	};

	T GetData(int pos)
	{
		Node<T>* node = GetNode();
		if (nullptr == node)
		{
			return nullptr;
		}

		return node->_data();
	};

	void Clear()
	{
		_head->Destroy();
	}

private:
	Node<T>* GetNode(int pos)	// pos의 위치에 있는 노드를 가져온다.
	{
		if (_size < pos)		//존재하지 않는 위치에 넣으려했다.
		{
			return nullptr;
		}

		Node<T>* current = _head;
		for (int i = 0; i < pos; i++)
		{
			current = current->_next;
		}

		return current;
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
			_next->_next->Destroy();

			delete _next;
			_next = nullptr;
		}
	};
};

