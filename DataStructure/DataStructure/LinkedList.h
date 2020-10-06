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
		//�Ǿտ� �߰��� ���
		if (0 == pos)
		{
			Node<T>* newNode	= new Node<T>(data);
			newNode->_next		= _head;
			_head				= newNode;

			return true;
		}

		//�Ǿ��� �ƴ� ���� �߰��� ���
		Node<T>* node = this.GetNode(pos - 1);

		if (nullptr == node) return false;		//��带 �������µ� �����ϸ� false�� �����ϰ� ����

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
	Node<T>* GetNode(int pos)	// pos�� ��ġ�� �ִ� ��带 �����´�.
	{
		if (_size < pos)		//�������� �ʴ� ��ġ�� �������ߴ�.
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

	//����Լ� ȣ��� �ڿ��ִ� ��嵵 ����
	void Destroy()
	{
		if (nullptr != _next) {
			_next->_next->Destroy();

			delete _next;
			_next = nullptr;
		}
	};
};

