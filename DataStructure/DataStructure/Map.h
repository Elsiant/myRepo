#pragma once

template<typename T1, typename T2>
class Node;


template<typename T1, typename T2>
class Map
{
private:
	Node* _top = nullptr;
	unsigned int _size = 0;
public:
	Map() {};
	~Map() { Clear(); };

	void Insert(T1 key, T2 value)
	{
		Node* newNode = new Node<T1, T2>(key, value);
		if (nullptr == _top)
		{
			_top = newNode();
			_size++;
		}
		else
		{
			unsigned int result = _top->InsertNode(newNode);
			_size += result;
		}
	};

	Node* Find(T1 key) {
		if (nullptr == _top)
		{
			return nullptr;
		}

		_top->Find(key);
	};

	void Erase(T1 key)
	{
		if (nullptr != _top)
		{
			unsigned int result = _top->EraseNode(key);
			_size -= result;

		}	
	};

	void Clear() {
		_size = 0;
		_top->Destroy();
	};

	bool IsEmpty() { return (0 == _size) ? true : false; };
	unsigned int GetSize() { return _size; };
};

//T1 key type T2 value type
template<typename T1, typename T2>
class Node
{
public:
	T1		_key;
	T2		_value;
	Node*	_left	= nullptr;
	Node*	_right	= nullptr;
	Node*	_parent	= nullptr;

public:
	Node(T1 key, T2 value) { _key = key; _value = value; };
	~Node() {};

	//재귀함수 호출로 뒤에있는 노드도 삭제
	void Destroy()
	{
		_parent = nullptr;

		if (nullptr != _left) {
			_left->Destroy();

			delete _left;
			_left = nullptr;
		}

		if (nullptr != _right) {
			_right->Destroy();

			delete _right;
			_right = nullptr;
		}
	};

	Node* Find(T1 key)
	{
		if (key == _key)
		{
			return *this;
		}

		if (key < _key)
		{
			if (nullptr == _left)
			{
				return nullptr;
			}

			return this->_left->Find(key);
		}
		else
		{
			if (nullptr == _right)
			{
				return nullptr;
			}

			return this->_right->Find(key);
		}
	}

	//size가 증가하면 1 리턴 아니면 0리턴
	int InsertNode(Node* node)
	{
		if (_key == node->_key)
		{
			_value = node->_value;
			return 0;
		}

		if (node->_key < _key)
		{
			if (nullptr == _left)
			{
				_left = node;
				node->_parent = this;
				return 1;
			}
			else
			{
				return _left->InsertNode(node);
			}
		}

		if (_key < node->_key)
		{
			if (nullptr == _right)
			{
				_right = node;
				node->_parent = this;
				return 1;
			}
			else
			{
				return _right->InsertNode(node);
			}
		}
	}

	void EraseNode(T1 key)
	{
		if (_key == key)
		{
			DeleteNode(this);
			return 1;
		}

		if (node->_key < _key)
		{
			if (nullptr == _left)
			{
				return 0;
			}

			return _left->EraseNode(key);
		}

		if (_key < node->_key)
		{
			if (nullptr == _right)
			{
				return 0;
			}

			return _right->EraseNode(key);
		}
	}

	void DeleteNode(Node* node)
	{
		if (node->_key < _key)
		{
			_left = node->_left;

			if (null != node->_right)
			{
				InsertNode(node->_right);
			}			
		}
		else
		{
			_right = node->_right;

			if (null != node->_left)
			{
				InsertNode(node->_left);
			}
		}
		
		delete node;
	}
};
